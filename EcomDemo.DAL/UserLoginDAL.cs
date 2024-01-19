using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomDemo.Model;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Net.Cache;

namespace EcomDemo.DAL
{
    public class UserLoginDAL
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;

        public bool UserLogin(UserLogin ul)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "UserLogin"));
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = ul.MobileNumber;
                        cmd.Parameters.Add(new SqlParameter("@Upassword", SqlDbType.VarChar)).Value = ul.Password;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if(dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    //username and id is for make cookies
                                    ul.UserName = dataReader["Uname"].ToString();
                                    ul.ID = Convert.ToInt32(dataReader["U_ID"]);
                                }
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        cmd.Connection.Close();  
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool CreateAccount(UserLogin ul)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfUserRegisteredThroughMobileNumber"));
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = ul.MobileNumber;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                result = false;
                            }
                            else
                            {
                                result = true;
                            }
                            cmd.Connection.Close();
                        }
                    }
                    if(result)
                    {
                        using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                        {
                            Random password = new Random();
                            string characters = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            //minumum length of the password should be 10 characters
                            int minlength = 6;
                            //declaring variable P to store the random password; it should be an empty string
                            string P = "";

                            for (int i = 0; i < minlength; i++)
                            {
                                int randomi = password.Next(characters.Length);
                                char randomchar = characters[randomi];
                                P += randomchar.ToString();
                            }
                            ul.Password = P;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "AddUser"));
                            cmd.Parameters.Add(new SqlParameter("@Uname", SqlDbType.VarChar, 100)).Value = ul.UserName;
                            cmd.Parameters.Add(new SqlParameter("@Uemail", SqlDbType.VarChar, 100)).Value = ul.Email;
                            cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar, 100)).Value = ul.MobileNumber;
                            cmd.Parameters.Add(new SqlParameter("@Uaddress", SqlDbType.VarChar, 100)).Value = ul.Address;
                            cmd.Parameters.Add(new SqlParameter("@Upassword", SqlDbType.VarChar)).Value = P;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                    

                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool ChangePassword(ChangePassword cp)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckUserOldPassword"));
                        cmd.Parameters.Add(new SqlParameter("@Uoldpassword", SqlDbType.VarChar)).Value = cp.OldPassword;
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = cp.mycookieID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        cmd.Connection.Close();
                    }
                    
                    if(result)
                    {
                        
                        using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "UserChangePassword"));
                            cmd.Parameters.Add(new SqlParameter("@Uoldpassword", SqlDbType.VarChar)).Value = cp.OldPassword;
                            cmd.Parameters.Add(new SqlParameter("@Unewpassword", SqlDbType.VarChar)).Value = cp.NewPassword;
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = cp.mycookieID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool UserForgotPassword(ForgotPassword fp)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckMobileNumberForForgotPassword"));
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = fp.MobileNumber;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    // fp.UserName = reader["Uname"].ToString();
                                    fp.ID = Convert.ToInt32(reader["U_ID"]);
                                    fp.Email = reader["Uemail"].ToString();
                                }
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                           
                        }
                        cmd.Connection.Close();
                    }
                    if(result)
                    {
                        using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                        {
                            Random password = new Random();
                            string characters = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            //minumum length of the password should be 10 characters
                            int minlength = 6;
                            //declaring variable P to store the random password; it should be an empty string
                            string P = "";

                            for (int i = 0; i < minlength; i++)
                            {
                                int randomi = password.Next(characters.Length);
                                char randomchar = characters[randomi];
                                P += randomchar.ToString();
                            }
                            fp.Body = P;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "UserForgotPasswordChanged"));
                            cmd.Parameters.Add(new SqlParameter("@Unewpassword", SqlDbType.VarChar)).Value = P;
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = fp.ID;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
