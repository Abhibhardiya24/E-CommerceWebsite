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
using System.Web;

namespace EcomDemo.DAL
{
    public class ProductMasterDAL
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;

        public AddEditProductModel GetProduct(AddEditProductModel aepm)
        {
            try
            
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProduct"));
                        cmd.Parameters.Add(new SqlParameter("@Pname", SqlDbType.VarChar)).Value = aepm.ProductName;
                        cmd.Parameters.Add(new SqlParameter("@Pdescription", SqlDbType.VarChar)).Value = aepm.ProductDescription;
                        cmd.Parameters.Add(new SqlParameter("@Pphoto", SqlDbType.VarChar)).Value = aepm.ProductPhoto;
                       // cmd.Parameters.Add(new SqlParameter("@Pdphoto", SqlDbType.VarChar)).Value = aepm.ProductDetailPhoto;
                        cmd.Parameters.Add(new SqlParameter("@Pstatus", SqlDbType.Bit)).Value = aepm.Status;
                        cmd.Parameters.Add(new SqlParameter("@Pcreatedate", SqlDbType.DateTime)).Value = aepm.CreateDate;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        
                        SqlDataReader reader = cmd.ExecuteReader();
                         List<ProductViewModel> productList = new List<ProductViewModel>();
                        while (reader.Read())
                        {
                            ProductViewModel pvm = new ProductViewModel();
                            pvm.ID = Convert.ToInt32(reader["P_ID"]);
                            pvm.ProductName = reader["Pname"].ToString();
                            pvm.ProductDescription = reader["Pdescription"].ToString();
                            pvm.ProductPhoto = reader["Pphoto"].ToString();
                            //pvm.ProductDetailPhoto = reader["Pdphoto"].ToString();
                            pvm.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                            pvm.Status = Convert.ToBoolean(reader["Pstatus"]);
                            pvm.CreateDate = reader["Pcreatedate"].ToString();
                            productList.Add(pvm);
                        }
                        aepm.productViewModel = productList;
                        cmd.Connection.Close();
                        return aepm;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public AddEditProductModel CreateProduct(AddEditProductModel aepm)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "AddProduct"));
                        cmd.Parameters.Add(new SqlParameter("@Pname", SqlDbType.VarChar)).Value = aepm.ProductName;
                        cmd.Parameters.Add(new SqlParameter("@Pdescription", SqlDbType.VarChar)).Value = aepm.ProductDescription;
                        cmd.Parameters.Add(new SqlParameter("@Pphoto", SqlDbType.VarChar)).Value = aepm.ProductPhoto;
                        cmd.Parameters.Add(new SqlParameter("@Pprice", SqlDbType.Decimal)).Value = aepm.ProductPrice;
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
            return aepm;
        }
        public AddEditProductModel EditProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "EditProduct"));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@P_ID",SqlDbType.Int)).Value = ID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            aepm.ID = Convert.ToInt32(reader["P_ID"]);
                            aepm.ProductName = reader["Pname"].ToString();   
                            aepm.ProductDescription = reader["Pdescription"].ToString();
                            aepm.ProductPhoto = reader["Pphoto"].ToString();
                            aepm.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                            aepm.Status = Convert.ToBoolean(reader["Pstatus"]);
                           
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return aepm;
        }
        
        public AddEditProductModel EditProduct(AddEditProductModel aepm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "EditProductDone"));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = aepm.ID;
                        cmd.Parameters.Add(new SqlParameter("@Pname", SqlDbType.VarChar)).Value = aepm.ProductName;
                        cmd.Parameters.Add(new SqlParameter("@Pdescription", SqlDbType.VarChar)).Value = aepm.ProductDescription;
                        cmd.Parameters.Add(new SqlParameter("@Pphoto", SqlDbType.VarChar)).Value = aepm.ProductPhoto;
                        cmd.Parameters.Add(new SqlParameter("@Pprice", SqlDbType.Decimal)).Value = aepm.ProductPrice;
                        cmd.Parameters.Add(new SqlParameter("@Pstatus", SqlDbType.Bit)).Value = aepm.Status;
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
            return aepm;
        }
        public AddEditProductModel DeleteProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "DeleteProduct"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
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
            
            return aepm;
        }
        public AddEditProductModel AddPhoto (AddEditProductModel aepm)
        {
          //  AddEditProductModel aepm = new AddEditProductModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "AddPhotoProductDetails"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = aepm.ID;
                        cmd.Parameters.Add(new SqlParameter("@Pdphoto", SqlDbType.VarChar)).Value = aepm.ProductDetailPhoto;
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
            return aepm;
        }

        public AddEditProductModel GetProductPhoto(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP",con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProductPhoto"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
                        cmd.Parameters.Add(new SqlParameter("@Pd_ID", SqlDbType.Int)).Value = aepm.PdID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        SqlDataReader dataReader = cmd.ExecuteReader();
                        List<ProductViewModel> productPhotoLists = new List<ProductViewModel>();
                        while(dataReader.Read())
                        {
                            ProductViewModel pvm = new ProductViewModel();
                            pvm.ID = Convert.ToInt32(dataReader["P_ID"]);
                            pvm.PdID = Convert.ToInt32(dataReader["Pd_ID"]);
                            pvm.ProductDetailPhoto = dataReader["Pdphoto"].ToString();
                            productPhotoLists.Add(pvm);
                        }
                        aepm.productViewModel = productPhotoLists;
                        cmd.Connection.Close();
                        return aepm;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public AddEditProductModel DeleteProductDetailPhoto(int PdID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "DeleteProductDetailPhoto"));
                        cmd.Parameters.Add(new SqlParameter("Pd_ID", SqlDbType.Int)).Value = PdID;
                        cmd.CommandTimeout = 0;
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
            return aepm;
        }
    }
}
