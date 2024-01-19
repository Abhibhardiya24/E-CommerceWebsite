using EcomDemo.DAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EcomDemo.BAL
{
    public class ProductMasterBAL
    {
        public AddEditProductModel GetProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.GetProduct(aepm);
            return aepm;
        }

        public AddEditProductModel CreateProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.CreateProduct(aepm); 
            return aepm;
        }

        public AddEditProductModel EditProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.EditProduct(ID);
            return aepm;
        }

        public AddEditProductModel EditProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.EditProduct(aepm);
            return aepm;
        }

        public AddEditProductModel DeleteProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.DeleteProduct(ID);
            return aepm;
        }

        public AddEditProductModel AddPhoto(AddEditProductModel aepm)
        {
           // AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm= pmd.AddPhoto(aepm);
            return aepm;
        }
        
        public AddEditProductModel GetProductPhoto(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.GetProductPhoto(ID);
            return aepm;
        }

        public AddEditProductModel DeleteProductDetailPhoto(int PdID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.DeleteProductDetailPhoto(PdID);
            return aepm;
        }


        ///////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////Product User BAl/////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////

        public UserShop GetProductsListsOnPage(UserShop us)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            us = pmd.GetProductsListsOnPage(us);
            return us;
        }

        public bool AddtoCart(UserShop us)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.AddtoCart(us);
            return result;
        }

        public UserShop InsertIntoCart(UserShop us)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            us = pmd.InsertIntoCart(us);
            return us;
        }
        public bool AddtoCartFromWishList(WishList wl)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.AddtoCartFromWishList(wl);
            return result;
        }
        public WishList InsertIntoCartFromWishList(WishList wl)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            wl = pmd.InsertIntoCartFromWishList(wl);
            return wl;
        }
        public bool AddWishList(UserShop us)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.AddWishList(us);
            return result;
        }

        public UserShop GetProductByID(int ID)
        {
            UserShop us = new UserShop();
            ProductMasterDAL pmd = new ProductMasterDAL();
            us = pmd.GetProductByID(ID);
            return us;
        }

        public UserShop CheckIfUserAddedProductInWishListsBefore(UserShop us, int ID)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            us = pmd.CheckIfUserAddedProductInWishListsBefore(us, ID);
            return us;
        }

        public bool ShowWishList(WishList wl)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.ShowWishList(wl);
            return result;
        }

        public WishList RemoveItemFromWishList(int ID)
        {
            WishList wl = new WishList();
            ProductMasterDAL pmd = new ProductMasterDAL();
            wl = pmd.RemoveItemFromWishList(ID);
            return wl;
        }

        public bool ShowMyCart(MyCart mc)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.ShowMyCart(mc); 
            return result;
        }

        public bool RemoveItemFromCart(int ID, MyCart mc)
        {
            bool result;
            //MyCart mc = new MyCart();
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.RemoveItemFromCart(ID, mc);
            return result;
        }

        public bool UpdateCart(int? PID,int? CMQty, MyCart mc)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.UpdateCart(PID, CMQty, mc);
            return result;
        }

        public bool ClickOnWishListHeartIcon(int productId, MyCart mc)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.ClickOnWishListHeartIcon(productId,mc);
            return result;
        }

        public bool GetValuesOnCheckOutPage(CheckOut co)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.GetValuesOnCheckOutPage(co);
            return result;
        }

        public bool PlaceOrder(CheckOut co)
        {
            bool result;
            ProductMasterDAL pmd = new ProductMasterDAL();
            result = pmd.PlaceOrder(co);
            return result;
        }

        public MyCart ShowViewCartInHeader(MyCart mc)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            mc = pmd.ShowViewCartInHeader(mc);
            return mc;
        }

        public OrderList MyOrders(OrderList ol)
        {
            ProductMasterDAL omd = new ProductMasterDAL();
            ol = omd.MyOrders(ol);
            return ol;
        }

        public OrderList MyOrderDetails(OrderList ol)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            ol = pmd.MyOrderDetails(ol);
            return ol;
        }

        public ContactWithUs ContactWithUs(ContactWithUs cwu)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            cwu = pmd.ContactWithUs(cwu);
            return cwu;
        }
    }
}
