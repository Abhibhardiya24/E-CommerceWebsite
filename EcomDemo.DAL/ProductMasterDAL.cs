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
using System.Net.Mail;
using System.Globalization;

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
                            pvm.SrNo = Convert.ToInt64(reader["SrNo"]);
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
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
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
        public AddEditProductModel AddPhoto(AddEditProductModel aepm)
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
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProductPhoto"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
                        cmd.Parameters.Add(new SqlParameter("@Pd_ID", SqlDbType.Int)).Value = aepm.PdID;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        SqlDataReader dataReader = cmd.ExecuteReader();
                        List<ProductViewModel> productPhotoLists = new List<ProductViewModel>();
                        while (dataReader.Read())
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


        ///////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////Product User DAl/////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        public UserShop GetProductsListsOnPage(UserShop us)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProductsListsOnPage"));
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<UserShop> list = new List<UserShop>();
                                while (reader.Read())
                                {
                                    UserShop usl = new UserShop();
                                    usl.PID = Convert.ToInt32(reader["P_ID"]);
                                    usl.ProductName = reader["Pname"].ToString();
                                    //us.ProductDescription = reader["Pdescription"].ToString();
                                    usl.ProductPhoto = reader["Pphoto"].ToString();
                                    usl.ProductDetailPhoto = reader["Pdphoto"].ToString();
                                    usl.ProductPrice = Convert.ToDecimal(reader["Pprice"]);
                                    list.Add(usl);
                                }
                                us.userShopList = list;
                            }
                            cmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return us;
        }

        public bool AddtoCart(UserShop us)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfSameProductInUserAlready"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = us.PID;
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    us.CmQty = Convert.ToInt32(reader["CmQty"]);
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

                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "UpdateCart"));
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = us.PID;
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                            cmd.Parameters.Add(new SqlParameter("@CmQty", SqlDbType.Int)).Value = us.CmQty;
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

        public UserShop InsertIntoCart(UserShop us)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "AddtoCart"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = us.PID;
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
            return us;
        }
        public bool AddtoCartFromWishList(WishList wl)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfSameProductInUserAlready"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = wl.PID;
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = wl.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    wl.CMQty = Convert.ToInt32(reader["CmQty"]);
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

                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "UpdateCart"));
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = wl.PID;
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = wl.UID;
                            cmd.Parameters.Add(new SqlParameter("@CmQty", SqlDbType.Int)).Value = wl.CMQty;
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

        public WishList InsertIntoCartFromWishList(WishList wl)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "AddtoCart"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = wl.UID;
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = wl.PID;
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
            return wl;
        }

        public bool AddWishList(UserShop us)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfUserAddedProductInWishListsBefore"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = us.PID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                }
                                result = false;
                            }
                            else
                            {
                                result = true;
                            }
                            cmd.Connection.Close();
                        }
                    }
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "AddWishList"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = us.PID;
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

        public UserShop GetProductByID(int ID)
        {
            UserShop us = new UserShop();
            try
            {
                us.PID = ID;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProductByID"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    us.PID = Convert.ToInt32(reader["P_ID"]);
                                   // us.CmQty = Convert.ToInt32(reader["CmQty"]);
                                    us.ProductName = reader["Pname"].ToString();
                                    us.ProductDescription = reader["Pdescription"].ToString();
                                    us.ProductPhoto = reader["Pphoto"].ToString();
                                    us.ProductPrice = Convert.ToDecimal(reader["Pprice"]);
                                }
                            }
                            cmd.Connection.Close();
                        }

                    }
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetProductDetails"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<UserShop> list = new List<UserShop>();
                                while (reader.Read())
                                {
                                    UserShop usl = new UserShop();
                                    usl.PDID = Convert.ToInt32(reader["Pd_ID"]);
                                    usl.ProductDetailPhoto = reader["Pdphoto"].ToString();
                                    list.Add(usl);
                                }
                                us.userShopList = list;
                            }
                            cmd.Connection.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return us;
        }

        public UserShop CheckIfUserAddedProductInWishListsBefore(UserShop us, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfUserAddedProductInWishListsBefore"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = us.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                }
                                us.WLCount = 1;
                            }
                            else
                            {
                                us.WLCount = 0;
                            }
                        }

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return us;
        }

        public bool ShowWishList(WishList wl)
        {
            bool result;
            try
            {
                
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "ShowWishListByID"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = wl.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reder = cmd.ExecuteReader())
                        {
                            if (reder.HasRows)
                            {
                                List<WishList> list = new List<WishList>();
                                while (reder.Read())
                                {
                                    WishList wishList = new WishList();
                                    wishList.ProductName = reder["Pname"].ToString();
                                    wishList.ProductPhoto = reder["Pphoto"].ToString();
                                    wishList.ProductPrice = Convert.ToInt32(reder["Pprice"]);
                                    wishList.PID = Convert.ToInt32(reder["P_ID"]);
                                    list.Add(wishList);
                                }
                                wl.wishList = list;
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

        public WishList RemoveItemFromWishList(int ID)
        {
            WishList wl = new WishList();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "RemoveItemFromWishList"));
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
            return wl;
        }

        public bool ShowMyCart(MyCart mc)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckItemInCart"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

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
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "ShowCart"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    List<MyCart> list = new List<MyCart>();
                                    while (reader.Read())
                                    {
                                        MyCart cart = new MyCart();
                                        cart.PID = Convert.ToInt32(reader["P_ID"]);
                                        cart.ProductPhoto = reader["Pphoto"].ToString();
                                        cart.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                                        cart.ProductName = reader["Pname"].ToString();
                                        cart.CMQty = Convert.ToInt32(reader["CmQty"]);
                                        list.Add(cart);
                                    }
                                    mc.myCartList = list;

                                }
                            }
                            cmd.Connection.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "CartAmountTotal"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        mc.SubTotal = Convert.ToInt32(reader["SubTotal"]);
                                    }
                                    mc.Cgst = Math.Round(mc.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                    mc.Sgst = Math.Round(mc.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                    mc.Igst = Math.Round(mc.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                    mc.TotalAmount = Math.Round(mc.SubTotal + mc.SubTotal * 7.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                }

                            }
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

        public bool RemoveItemFromCart(int ID, MyCart mc)
        {
            bool result;

            try
            {

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckItemInCart"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

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
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "RemoveItemFromCart"));
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = ID;
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

        public bool UpdateCart(int? PID, int? CMQty, MyCart mc)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckItemInCart"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

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
                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.Add(new SqlParameter("@mode", "UpdateItemInCart"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = PID;
                            cmd.Parameters.Add(new SqlParameter("@CmQty", SqlDbType.Int)).Value = CMQty;
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

        public bool ClickOnWishListHeartIcon(int productId, MyCart mc)
        {
            bool result;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "CheckIfUserAddedProductInWishListsBefore"));
                        cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = productId;
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = mc.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

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

                    if (result)
                    {
                        using (SqlCommand cmd = new SqlCommand("ProductMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "RemoveItemFromWishList"));
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = productId;
                            cmd.Parameters.Add(new SqlParameter("U_ID", SqlDbType.Int)).Value = mc.UID;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                    if (!result)
                    {
                        using (SqlCommand cmd = new SqlCommand("CartMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "AddWishList"));
                            cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = productId;
                            cmd.Parameters.Add(new SqlParameter("U_ID", SqlDbType.Int)).Value = mc.UID;
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

        public bool GetValuesOnCheckOutPage(CheckOut co)
        {
            bool result;
            try
            {
               
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetUserDataOnCheckOutPage"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    co.UserName = reader["Uname"].ToString();
                                    co.Address = reader["Uaddress"].ToString();
                                    co.Email = reader["Uemail"].ToString();
                                    co.MobileNumber = reader["Ucontact"].ToString();
                                }
                            }
                        }
                        cmd.Connection.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetUserCartItems"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<CheckOut> checkout = new List<CheckOut>();
                                int totalQuantity = 0;
                                while (reader.Read())
                                {
                                    CheckOut ckout = new CheckOut();
                                    ckout.CMQty = Convert.ToInt32(reader["CmQty"]);
                                    ckout.ProductName = reader["Pname"].ToString();
                                    ckout.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                                    //ckout.PID = Convert.ToInt32(reader["P_ID"]);
                                    //ckout.Productdescription = reader["Pdescription"].ToString();
                                    //ckout.ProductPhoto = reader["Pphoto"].ToString();
                                    //ckout.ProductQuantity = Convert.ToInt32(reader["CmQty"]);
                                    totalQuantity = totalQuantity + ckout.CMQty;
                                    checkout.Add(ckout);
                                }
                                co.checkOutList = checkout;
                                co.TotalQty = totalQuantity;
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
                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "CartAmountTotal"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        co.SubTotal = Convert.ToInt32(reader["SubTotal"]);
                                    }
                                }
                                co.Cgst = Math.Round(co.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                co.Sgst = Math.Round(co.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                co.Igst = Math.Round(co.SubTotal * 2.5m / 100m, 2, MidpointRounding.AwayFromZero);
                                co.TotalAmount = Math.Round(co.SubTotal + co.SubTotal * 7.5m / 100m, 2, MidpointRounding.AwayFromZero);
                            }
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
        private string GenerateBillNo()
        {
            Random GenerateBillNo = new Random();
            string BN = "1234567890";
            int minLength = 8;
            string BillNo = " ";
            for (int i = 0; i < minLength; i++)
            {
                int randomI = GenerateBillNo.Next(BN.Length);
                char randomChar = BN[randomI];
                BillNo += randomChar;
            }
            
            return BillNo;
        }
        private string GenerateTransactionID()
        {
            Random GenerateTransactionID = new Random();
            string TID = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int mLength = 8;
            string TransactionID = " ";
            for (int i = 0; i < mLength; i++)
            {
                int randomI = GenerateTransactionID.Next(TID.Length);
                char randomChar = TID[randomI];
                TransactionID += randomChar;
            }
            return TransactionID;
        }
        public bool PlaceOrder(CheckOut co)
        {
            bool result;
            try
            {
                
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string BillNo = GenerateBillNo();
                    string TransactionID = GenerateTransactionID();
                    // bool isDuplicateBillNo = false;
                    int isDuplicateBillNo;
                    int isDuplicateTransactionID = 0;
                    do
                    {
                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            co.BillNo = Convert.ToInt32(BillNo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "CheckBillNoIfSame"));
                            cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = BillNo;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            isDuplicateBillNo = Convert.ToInt32(cmd.ExecuteScalar());

                            if (isDuplicateBillNo > 0)
                            {
                                BillNo = GenerateBillNo();
                            }
                            cmd.Connection.Close();
                        }
                    }
                    while (isDuplicateBillNo > 0);

                    do
                    {
                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "CheckTransactionIDIfSame"));
                            cmd.Parameters.Add(new SqlParameter("@TransactionID", SqlDbType.VarChar,50)).Value = TransactionID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            isDuplicateTransactionID = Convert.ToInt32(cmd.ExecuteScalar());
                            if (isDuplicateTransactionID > 0)
                            {
                                TransactionID = GenerateTransactionID();
                            }
                            cmd.Connection.Close();
                        }
                    }
                    while (isDuplicateTransactionID > 0);

                    result = true;
                    if (result)
                    {


                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "PlaceOrder"));
                            cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = BillNo;
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                            cmd.Parameters.Add(new SqlParameter("@Uname", SqlDbType.VarChar)).Value = co.UserName;
                            cmd.Parameters.Add(new SqlParameter("@Uemail", SqlDbType.VarChar)).Value = co.Email;
                            cmd.Parameters.Add(new SqlParameter("@Ucontact", SqlDbType.VarChar)).Value = co.MobileNumber;
                            cmd.Parameters.Add(new SqlParameter("@Uaddress", SqlDbType.VarChar)).Value = co.Address;
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", SqlDbType.VarChar)).Value = co.PaymentMode;
                            cmd.Parameters.Add(new SqlParameter("@TransactionID", SqlDbType.VarChar)).Value = TransactionID;
                            cmd.Parameters.Add(new SqlParameter("@SubAmount", SqlDbType.Decimal)).Value = co.SubTotal;
                            cmd.Parameters.Add(new SqlParameter("@TotalQty", SqlDbType.Int)).Value = co.TotalQty;
                            cmd.Parameters.Add(new SqlParameter("@Cgst", SqlDbType.Decimal)).Value = co.Cgst;
                            cmd.Parameters.Add(new SqlParameter("@Sgst", SqlDbType.Decimal)).Value = co.Sgst;
                            cmd.Parameters.Add(new SqlParameter("@Igst", SqlDbType.Decimal)).Value = co.Igst;
                            cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Decimal)).Value = co.TotalAmount;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                        
                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "GetUserCartItems"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    List<CheckOut> productLists = new List<CheckOut>();
                                    while (reader.Read())
                                    {
                                        CheckOut cko = new CheckOut();
                                        cko.PID = Convert.ToInt32(reader["P_ID"]);
                                        cko.ProductName = reader["Pname"].ToString();
                                        cko.Productdescription = reader["Pdescription"].ToString();
                                        cko.ProductPhoto = reader["Pphoto"].ToString();
                                        cko.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                                        cko.ProductQuantity = Convert.ToInt32(reader["CmQty"]);
                                        productLists.Add(cko);
                                    }
                                    co.productLists = productLists;

                                }
                            }
                            cmd.Connection.Close();
                        }
                        for (int i = 0; i < co.productLists.Count; i++)
                        {
                            using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@mode", "InsertIntoOrderDetails"));
                                cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = BillNo;
                                cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                                cmd.Parameters.Add(new SqlParameter("@P_ID", SqlDbType.Int)).Value = co.productLists[i].PID;
                                cmd.Parameters.Add(new SqlParameter("@Pname", SqlDbType.VarChar)).Value = co.productLists[i].ProductName;
                                cmd.Parameters.Add(new SqlParameter("@Pdecription", SqlDbType.VarChar)).Value = co.productLists[i].Productdescription;
                                cmd.Parameters.Add(new SqlParameter("@Pphoto", SqlDbType.VarChar)).Value = co.productLists[i].ProductPhoto;
                                cmd.Parameters.Add(new SqlParameter("@PQty", SqlDbType.Int)).Value = co.productLists[i].ProductQuantity;
                                cmd.Parameters.Add(new SqlParameter("@Pprice", SqlDbType.Int)).Value = co.productLists[i].ProductPrice;
                                co.productLists[i].OneProductTotalAmount = co.productLists[i].ProductQuantity * co.productLists[i].ProductPrice;
                                cmd.Parameters.Add(new SqlParameter("@TotalAmount", SqlDbType.Int)).Value = co.productLists[i].OneProductTotalAmount;
                                cmd.CommandTimeout = 0;
                                cmd.Connection.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Connection.Close();
                            }
                        }
                        using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@mode", "DeleteItemsFromCartMasterByUserID"));
                            cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = co.UID;
                            cmd.CommandTimeout = 0;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                            
                        }
                    }
                    else
                    {
                         result = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public MyCart ShowViewCartInHeader(MyCart mc)
        {
            try
            {
                List<MyCart> myCartLists = new List<MyCart>();
                mc.myCartList = myCartLists;
            }
            catch (Exception)
            {

                throw;
            }
            return mc;
        }

        public OrderList MyOrders(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetOrderListsByUserID"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = ol.UID;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                               List<OrderList> lists = new List<OrderList>();
                                while(reader.Read())
                                {
                                    OrderList myOrders = new OrderList();
                                    myOrders.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    myOrders.BillNo = Convert.ToInt32(reader["BillNo"]);
                                    myOrders.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    myOrders.TransactionID = reader["TransactionID"].ToString();
                                    myOrders.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    myOrders.Status = Convert.ToInt32(reader["Omstatus"]);
                                    myOrders.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    myOrders.OrderDate = Convert.ToDateTime(reader["Omcreatedate"]);
                                    lists.Add(myOrders);
                                }
                                ol.orderLists = lists;
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
            return ol;
        }

        public OrderList MyOrderDetails(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "OrderLists"));
                        cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = ol.BillNo;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<OrderList> list = new List<OrderList>();
                                while (reader.Read())
                                {
                                    OrderList orderList = new OrderList();
                                    ol.UID = Convert.ToInt32(reader["U_ID"]);
                                    ol.UserName = reader["Uname"].ToString();
                                    ol.Email = reader["Uemail"].ToString();
                                    ol.MobileNumber = reader["Ucontact"].ToString();
                                    ol.Address = reader["Uaddress"].ToString();
                                    ol.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    ol.TransactionID = reader["TransactionID"].ToString();
                                    ol.SubTotal = Convert.ToDecimal(reader["SubAmount"]);
                                    ol.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    ol.Cgst = Convert.ToDecimal(reader["Cgst"]);
                                    ol.Sgst = Convert.ToDecimal(reader["Sgst"]);
                                    ol.Igst = Convert.ToDecimal(reader["Igst"]);
                                    ol.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    ol.Status = Convert.ToInt32(reader["Omstatus"]);
                                    ol.OrderDate = Convert.ToDateTime(reader["OrderCreateDate"]);
                                    orderList.PID = Convert.ToInt32(reader["P_ID"]);
                                    orderList.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    orderList.ProductName = reader["Pname"].ToString();
                                    orderList.ProductDescription = reader["Pdecription"].ToString();
                                    orderList.ProductPhoto = reader["Pphoto"].ToString();
                                    orderList.ProductQuantity = Convert.ToInt32(reader["PQty"]);
                                    orderList.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                                    list.Add(orderList);
                                }
                                ol.orderLists = list;
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
            return ol;
        }

        public ContactWithUs ContactWithUs(ContactWithUs cwu)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ContactMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "ContactWithUs"));
                        cmd.Parameters.Add(new SqlParameter("@U_ID", SqlDbType.Int)).Value = cwu.UID;
                        cmd.Parameters.Add(new SqlParameter("@CO_Name", SqlDbType.VarChar)).Value = cwu.Name;
                        cmd.Parameters.Add(new SqlParameter("@CO_MobileNumber", SqlDbType.VarChar)).Value = cwu.MobileNumber;
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = cwu.Email;
                        cmd.Parameters.Add(new SqlParameter("@CO_Subject", SqlDbType.VarChar, 500)).Value = cwu.Subject;
                        cmd.Parameters.Add(new SqlParameter("@CO_Message", SqlDbType.VarChar, 1000)).Value = cwu.Message;
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
            return cwu;
        }
    }
}
