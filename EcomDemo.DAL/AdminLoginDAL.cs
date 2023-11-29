using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using EcomDemo.Model;
using System.Collections.Generic;
using System.Web;
using System.Reflection;
using System.Diagnostics.Eventing.Reader;

namespace EcomDemo.DAL
{
    public class AdminLoginDAL
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;
        public bool AdminLogin(AdminViewModel avm)
        {
            //AdminViewModel model = new AdminViewModel();
            bool result;
            try
            { 
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AdminMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "Alogin"));
                        cmd.Parameters.Add(new SqlParameter("@Ausername", SqlDbType.VarChar)).Value = avm.UserName;
                        cmd.Parameters.Add(new SqlParameter("@Apassword", SqlDbType.VarChar)).Value = avm.Password;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if(dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ChangePassword cp = new ChangePassword();
                                    avm.UserName = dataReader["Ausername"].ToString();
                                    avm.Password = dataReader["APassword"].ToString();
                                    avm.ID = Convert.ToInt32(dataReader["AID"]);
                                    cp.mycookieID = Convert.ToInt32(dataReader["AID"]);
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
        public bool ChangePassword(ChangePassword cp)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AdminMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckOldPassword"));
                        cmd.Parameters.Add(new SqlParameter("@AOldPassword", SqlDbType.VarChar)).Value = cp.OldPassword;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cp.OldPassword = reader["Apassword"].ToString();
                                }
                                result = true;
                                
                            }
                            else
                            {
                                result = false;
                            }
                            cmd.Connection.Close();
                        }
                    }
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("AdminMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "ChangePassword"));
                            cmd.Parameters.Add(new SqlParameter("@AOldPassword", SqlDbType.VarChar)).Value = cp.OldPassword;
                            cmd.Parameters.Add(new SqlParameter("@ANewpassword", SqlDbType.Int)).Value = cp.NewPassword;
                            cmd.Parameters.Add(new SqlParameter("@AID", SqlDbType.Int)).Value = cp.mycookieID;
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

        public bool GetUserNameFP(ForgotPassword fp)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("AdminMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckUserNameForForgotPassword"));
                        cmd.Parameters.Add(new SqlParameter("@Ausername", SqlDbType.VarChar)).Value = fp.UserName;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    fp.UserName = reader["Ausername"].ToString();
                                    fp.Email = reader["Aemail"].ToString();
                                }

                                result = true;
                                
                            }
                            else
                            {
                                result = false;
                            }
                           
                        }
                        cmd.Connection.Close() ;
                    }
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("AdminMasterSP", con))
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
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "ForgotPasswordChanged"));
                            cmd.Parameters.Add(new SqlParameter("@Apassword", SqlDbType.VarChar)).Value = P;
                            cmd.ExecuteNonQuery();
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
