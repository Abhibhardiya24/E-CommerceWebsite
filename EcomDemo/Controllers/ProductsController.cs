using EcomDemo.BAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomDemo.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(UserShop us)
        {
            
                ProductMasterBAL pmb = new ProductMasterBAL();
                us = pmb.GetProductsListsOnPage(us);
                return View(us);
            
            
        }

        public ActionResult AddtoCart(UserShop us)
        {
            bool result;
            if (Request.Cookies["CookieByUserName"] != null)
            {
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                us.UID = UID;
                try
                {
                        ProductMasterBAL pmb = new ProductMasterBAL();
                        result = pmb.AddtoCart(us);
                    if (result)
                    {
                        TempData["ItemAddedInCart"] = "ItemAddedInCart";
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        ProductMasterBAL InsertIntoCart = new ProductMasterBAL();
                        us = InsertIntoCart.InsertIntoCart(us);
                        TempData["ItemAddedInCart"] = "ItemAddedInCart";
                        return RedirectToAction("Index", "Products");
                    }    
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["LoginToContinue"] = "LoginToContinue";
            return RedirectToAction("Index", "MyAccount");
        }

        public ActionResult AddtoCartFromWishList(WishList wl, int? PID)
        {
            bool result;
            try
            {
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                wl.UID = UID;
                ProductMasterBAL pmb = new ProductMasterBAL();
                wl.PID = PID.Value;
                result = pmb.AddtoCartFromWishList(wl);
                if (result)
                {
                    TempData["ItemAddedInCart"] = "ItemAddedInCart";
                    return RedirectToAction("WishList", "MyAccount");
                }
                else
                {
                    ProductMasterBAL pmba = new ProductMasterBAL();
                    wl = pmba.InsertIntoCartFromWishList(wl);
                    TempData["ItemAddedInCart"] = "ItemAddedInCart";
                    return RedirectToAction("WishList", "MyAccount");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public ActionResult AddWishList(int ID)
        {
            UserShop us = new UserShop();
            bool result;
            if (Request.Cookies["CookieByUserName"] != null)
            {
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                us.UID = UID;
                us.PID = ID;
                try
                {
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    result = pmb.AddWishList(us);
                    if(result)
                    {
                        TempData["AddedIntoWishList"] = "AddedIntoWishList";
                        return RedirectToAction("Index", "Products");
                    }
                    else
                    {
                        TempData["AlreadyAddedWishList"] = "AlreadyAddedWishList";
                        return RedirectToAction("Index", "Products");
                    } 
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["LoginForWishList"] = "LoginForWishList";
            return RedirectToAction("Index", "MyAccount");
        }
        public ActionResult ProductDetails(int ID)
        {
            
            UserShop us = new UserShop();
            us.PID = ID;
            ProductMasterBAL pmb = new ProductMasterBAL();
            us = pmb.GetProductByID(ID);
            if (Request.Cookies["CookieByUserName"] != null)
            {
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                us.UID = UID;
                ProductMasterBAL pmdb = new ProductMasterBAL();
                us = pmdb.CheckIfUserAddedProductInWishListsBefore(us, ID);
                return View(us);
            }
            return View(us);
        }
    }
}