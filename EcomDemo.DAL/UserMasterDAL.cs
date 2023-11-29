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

namespace EcomDemo.DAL
{
    public class UserMasterDAL
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;

        public AddEditUserModel UserCreate(AddEditUserModel aeum)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                       // DateTime currenttime = DateTime.Now;
                       // aeum.CreateDate = currenttime.ToString();
                        cmd.Parameters.Add(new SqlParameter("@mode", "AddUser"));
                        cmd.Parameters.Add(new SqlParameter("@Uname", SqlDbType.VarChar)).Value = aeum.UserName;
                        cmd.Parameters.Add(new SqlParameter("@Uemail", SqlDbType.VarChar)).Value = aeum.Email;
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = aeum.Contact;
                        cmd.Parameters.Add(new SqlParameter("@Uaddress", SqlDbType.VarChar)).Value = aeum.Address;
                        cmd.Parameters.Add(new SqlParameter("@Upassword", SqlDbType.VarChar)).Value = aeum.Password;
                       // cmd.Parameters.Add(new SqlParameter("@Ustatus", SqlDbType.Bit)).Value = true;
                       // cmd.Parameters.Add(new SqlParameter("@Ucreatedate", SqlDbType.VarChar)).Value = currenttime;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return aeum;
        }

        public AddEditUserModel UserGet(AddEditUserModel aeum)
        {
           
            try
            {
               
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetUser"));
                        cmd.Parameters.Add(new SqlParameter("@Uname", SqlDbType.VarChar)).Value = aeum.UserName;
                        cmd.Parameters.Add(new SqlParameter("@Uemail", SqlDbType.VarChar)).Value = aeum.Email;
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = aeum.Contact;
                        cmd.Parameters.Add(new SqlParameter("@Uaddress", SqlDbType.VarChar)).Value = aeum.Address;
                        cmd.Parameters.Add(new SqlParameter("@Ustatus", SqlDbType.Bit)).Value = aeum.Status;
                        cmd.Parameters.Add(new SqlParameter("@Ucreatedate", SqlDbType.DateTime)).Value = aeum.CreateDate;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        List<AdminViewModel> adminList = new List<AdminViewModel>();
                        while (reader.Read())
                        {
                            AdminViewModel avm = new AdminViewModel();
                            avm.ID = Convert.ToInt32(reader["U_ID"]);
                            avm.UserName = reader["Uname"].ToString();
                            avm.Email = reader["Uemail"].ToString();
                            avm.Contact = reader["Ucontact"].ToString();
                            avm.Address = reader["Uaddress"].ToString();
                            avm.Status = Convert.ToBoolean(reader["Ustatus"]);
                            avm.CreateDate = reader["Ucreatedate"].ToString();
                            adminList.Add(avm);
                        }
                        aeum.adminViewModel = adminList;
                        cmd.Connection.Close();
                    }
                }           
            }
            catch (Exception)
            {

                throw;
            }
            return aeum;
        }

        public AddEditUserModel UserEditDone(int ID)
        {
            AddEditUserModel aeum = new AddEditUserModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetEditUser"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        List<AdminViewModel> adminList = new List<AdminViewModel>();
                        while (reader.Read())
                        {
                            AdminViewModel avm = new AdminViewModel();
                            aeum.ID = Convert.ToInt32(reader["U_ID"]);
                            aeum.UserName = reader["Uname"].ToString();
                            aeum.Email = reader["Uemail"].ToString();
                            aeum.Contact = reader["Ucontact"].ToString();
                            aeum.Address = reader["Uaddress"].ToString();
                            aeum.Status = Convert.ToBoolean(reader["Ustatus"]);
                            
                            //adminList.Add(avm);
                        }
                        //aeum.adminViewModel = adminList;
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return aeum;
        }
        public AddEditUserModel UserEditDone(AddEditUserModel aeum)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetEditDone"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = aeum.ID;
                        cmd.Parameters.Add(new SqlParameter("@Uname", SqlDbType.VarChar)).Value = aeum.UserName;
                        cmd.Parameters.Add(new SqlParameter("@Uemail", SqlDbType.VarChar)).Value = aeum.Email;
                        cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = aeum.Contact;
                        cmd.Parameters.Add(new SqlParameter("@Uaddress", SqlDbType.VarChar)).Value = aeum.Address;
                        cmd.Parameters.Add(new SqlParameter("@Ustatus", SqlDbType.VarChar)).Value = aeum.Status;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return aeum;
        }

        public AddEditUserModel DeleteUser(int ID)
        {
            AddEditUserModel aeum = new AddEditUserModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "DeleteUser"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = ID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return aeum;
        }
    }
}
