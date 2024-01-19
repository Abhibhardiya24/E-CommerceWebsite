using EcomDemo.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcomDemo.Model;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Dynamic;

namespace EcomDemo.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            //dynamic multipleModel = new ExpandoObject();
            //multipleModel.myCarts = GetMyCarts();
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin ul)
        {
            bool result;
            try
            {
                UserLoginBAL ulb = new UserLoginBAL();
                result = ulb.UserLogin(ul);
                if (result)
                {
                    //dynamic multipleModel = new ExpandoObject();
                    //multipleModel.myCarts = GetMyCarts();
                    HttpCookie cookieID = new HttpCookie("UserCookieByID", ul.ID.ToString());
                    Response.Cookies.Add(cookieID);
                    HttpCookie cookieUserName = new HttpCookie("CookieByUserName", ul.UserName.ToString());
                    Response.Cookies.Add(cookieUserName);
                    string userName;
                    userName = Request.Cookies["CookieByUserName"].Value;
                    //ViewBag.UserName = userName;    
                    // string returnUrl = Url.Action("Index", "MyAccount");
                    // return RedirectToAction("Index", "Home", new { returnUrl });
                    return RedirectToAction("Index", "Home", new { userName });
                   // return RedirectToAction("Index", "Home");

                }
                else
                {
                    TempData["LoginError"] = "LoginError";
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index", "MyAccount");
            //return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserLogin ul)
        {
            bool result;
            UserLoginBAL ulb = new UserLoginBAL();
            result = ulb.CreateAccount(ul);
            if (result)
            {

                MailMessage mail = new MailMessage("abhibhardiya@gmail.com", ul.Email);
                mail.Body = "Congratulations! You have successfully created account. <br/> " +
                             "Your Generated Password is: <b>" + ul.Password + "</b>";
                mail.Subject = "You have created account in OnlineDukan!";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential("abhibhardiya@gmail.com", "xqjagglnduidliqv");
                smtp.Credentials = nc;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                TempData["AccountCreated"] = "AccountCreated";
                return RedirectToAction("Index", "MyAccount");
            }
            else
            {
                TempData["UserAlreadyRegistered"] = "UserAlreadyRegistered";
                return RedirectToAction("Index", "MyAccount");
            }



        }

        public ActionResult LogOut()
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                HttpCookie cookieByUsername = new HttpCookie("CookieByUserName");
                cookieByUsername.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(cookieByUsername);
                HttpCookie cookieID = new HttpCookie("UserCookieByID");
                cookieID.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(cookieID);
                return RedirectToAction("Index", "MyAccount");
            }
            return RedirectToAction("Index", "MyAccount");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                //dynamic multipleModel = new ExpandoObject();
                //multipleModel.myCarts = GetMyCarts();
                ChangePassword cp = new ChangePassword();
                //string Uid = Request.Cookies["UserCookieByID"].Value;
                //cp.mycookieID = Convert.ToInt32(Uid);
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cp)
        {
            int Uid = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
            cp.mycookieID = Uid;
            UserLoginBAL ulb = new UserLoginBAL();
            bool result = ulb.ChangePassword(cp);
            if (result)
            {
                HttpCookie myCookieUsername = new HttpCookie("CookieByUserName");
                myCookieUsername.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(myCookieUsername);
                HttpCookie myCookieID = new HttpCookie("UserCookieByID");
                myCookieID.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(myCookieID);
                TempData["ChangedPassword"] = "ChangedPassword";
                return RedirectToAction("Index", "MyAccount");
            }
            else
            {
                ViewBag.ChangePasswordError = "";
            }
            return View(cp);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword fp)
        {
            UserLoginBAL ulb = new UserLoginBAL();
            bool result = ulb.UserForgotPassword(fp);

            if (result)
            {

                MailMessage mail = new MailMessage("abhibhardiya@gmail.com", fp.Email);
                mail.Body = "Your New Automated Generated Password is: <b>" + fp.Body + "</b>";
                mail.Subject = "From OnlineDukan!";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential("abhibhardiya@gmail.com", "xqjagglnduidliqv");
                smtp.Credentials = nc;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                TempData["ForgotPasswordChanged"] = "ForgotPasswordChanged";
                return RedirectToAction("Index", "MyAccount");

            }
            else
            {
                ViewBag.ForgotPasswordError = "";
            }

            return View(fp);
        }

        public ActionResult MyCart(MyCart mc)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    //dynamic multipleModel = new ExpandoObject();
                    //multipleModel.myCarts = GetMyCarts();
                    bool result;
                    int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                    mc.UID = UID;
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    result = pmb.ShowMyCart(mc);
                    if (result)
                    {
                        return View(mc);
                    }
                    else
                    {
                        //TempData["NoItemInCart"] = "NoItemInCart";
                        return View(mc);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            TempData["LoginForCart"] = "LoginForCart";
            return RedirectToAction("Index", "MyAccount");
        }
        public ActionResult RemoveItemFromCart(int ID)
        {
            MyCart mc = new MyCart();

            try
            {
                bool result;
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                mc.UID = UID;
                ProductMasterBAL pmb = new ProductMasterBAL();
                result = pmb.RemoveItemFromCart(ID, mc);
                if (result)
                {
                    TempData["RemoveItemFromCart"] = "RemoveItemFromCart";
                    return RedirectToAction("MyCart", "MyAccount");
                }

            }
            catch (Exception)
            {
                throw;
            }

            TempData["NoItemInCart"] = "NoItemInCart";
            return RedirectToAction("MyCart", "MyAccount");
        }
        public ActionResult WishList(WishList wl)
        {

            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    bool result;
                    int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                    wl.UID = UID;
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    result = pmb.ShowWishList(wl);
                    if(result)
                    {
                        return View(wl);
                    }
                    else
                    {
                        TempData["NoItemInWishList"] = "NoItemInWishList";
                        return View(wl);
                    }
                    
                }
                catch (Exception)
                {

                    throw;
                }
            }
            TempData["ShowWishList"] = "ShowWishList";
            return RedirectToAction("Index", "MyAccount");

        }

        public ActionResult RemoveItemFromWishList(int ID)
        {
            WishList wl = new WishList();
            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    wl = pmb.RemoveItemFromWishList(ID);
                    TempData["RemoveItemFromWishList"] = "RemoveItemFromWishList";
                    return RedirectToAction("WishList", "MyAccount");
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return RedirectToAction("Login", "MyAccount");
        }

        public ActionResult UpdateCart(int? PID, int? CMQty, MyCart mc)
        {
            //MyCart mc = new MyCart();
            try
            {
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                mc.UID = UID;
                bool result;
                ProductMasterBAL pmb = new ProductMasterBAL();
                result = pmb.UpdateCart(PID, CMQty, mc);
                if (result)
                {
                    TempData["CartUpdated"] = "CartUpdated";
                    return RedirectToAction("MyCart", "MyAccount");
                }
                else
                {
                    TempData["NoItemInCart"] = "NoItemInCart";
                    return RedirectToAction("MyCart", "MyAccount");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public JsonResult ClickOnWishListHeartIcon(int productId)
        {
            bool result;
            MyCart mc = new MyCart();
            int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
            mc.UID = UID;
            ProductMasterBAL pmb = new ProductMasterBAL();
            result = pmb.ClickOnWishListHeartIcon(productId, mc);
            //TempData["AddedIntoWishList"] = true;
            return Json(result);
        }
        public ActionResult Checkout(CheckOut co)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                bool result;
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                co.UID = UID;
                ProductMasterBAL pmb = new ProductMasterBAL();
                result = pmb.GetValuesOnCheckOutPage(co);
                if (result)
                {
                    return View(co);
                }
                else
                {
                    TempData["NoItemInCheckOut"] = "NoItemInCheckOut";
                    return RedirectToAction("Index", "Products");
                }
            }
            TempData["CheckOut"] = "CheckOut";
            return RedirectToAction("Index", "MyAccount");
        }

        [HttpGet]
        public ActionResult PlaceOrder()
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "MyAccount");
        }
        [HttpPost]
        public ActionResult PlaceOrder(CheckOut co)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                bool result;
                int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                co.UID = UID;
                ProductMasterBAL pmb = new ProductMasterBAL();
                result = pmb.PlaceOrder(co);
                if (result)
                {
                    List<CheckOut> productLists = new List<CheckOut>();
                    StringBuilder bodyBuilder = new StringBuilder();
                    bodyBuilder.Append("Congratulations! Your Order Has Been Placed!<br/><br/>");
                    bodyBuilder.Append("Your Bill No is : <b style=\"color: red;\">" + co.BillNo + "</b>" + "<br/><br/>");
                    bodyBuilder.Append("Ordered Item/s:<br/><br/>");
                    foreach (var item in co.productLists)
                    {
                        bodyBuilder.Append($"{item.ProductName}:&nbsp;");
                        bodyBuilder.Append($" {item.ProductQuantity} * ₹{item.ProductPrice.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1)} = ₹{item.OneProductTotalAmount.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1)}" + "<br/>");
                    }
                    bodyBuilder.Append("<br/>");
                    bodyBuilder.Append($"CGST(2.5%) : ₹{co.Cgst.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1)}<br/>");
                    bodyBuilder.Append($"SGST(2.5%) : ₹{co.Sgst.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1)}<br/>");
                    bodyBuilder.Append($"IGST(2.5%) : ₹{co.Igst.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1)}<br/><br/>");
                    MailMessage mail = new MailMessage("abhibhardiya@gmail.com", co.Email);
                    mail.Body = bodyBuilder.ToString() + "<br/>Your Total Amount is: <b style=\"color: blue;\">₹" + co.TotalAmount.ToString("C", new System.Globalization.CultureInfo("en-IN")).Substring(1) + "</b>" + " (Including Gst)";
                    mail.Subject = "From OnlineDukan!";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential nc = new NetworkCredential("abhibhardiya@gmail.com", "xqjagglnduidliqv");
                    smtp.Credentials = nc;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                    TempData["NewOrder"] = "NewOrder";
                    return RedirectToAction("MyOrderDetails", "MyAccount", new {co.BillNo });
                   // return View(co);
                }

            }

            return RedirectToAction("Index", "MyAccount");
        }

        //public List<MyCart> GetMyCarts()
        //{
        //    List<MyCart> myCarts = new List<MyCart>();
        //    return myCarts;
        //}

        //public List<CheckOut> GetCheckOuts()
        //{
        //    List<CheckOut> checkOuts = new List<CheckOut>();
        //    return checkOuts;
        //}
        
        //public List<WishList> wishLists ()
        //{
        //    List<WishList> wishLists = new List<WishList>();
        //    return wishLists;
        //}

        public ActionResult MyOrders(OrderList ol)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                    ol.UID = UID;
                    var UserName = Request.Cookies["CookieByUserName"].Value.ToString();
                    ol.UserName = UserName;
                    ProductMasterBAL omb = new ProductMasterBAL();
                    ol = omb.MyOrders(ol);
                    return View(ol);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            TempData["MyOrders"] = "MyOrders";
            return RedirectToAction("Index", "MyAccount");
        }

        public ActionResult MyOrderDetails(int BillNo)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    OrderList ol = new OrderList();
                    ol.BillNo = BillNo;
                    int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                    ol.UID = UID;
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    ol = pmb.MyOrderDetails(ol);
                    return View(ol);
                }   
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "MyAccount");
        }

        [HttpGet]
        public ActionResult ContactWithUs()
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                return View();
            }
            TempData["ContactWithUs"] = "ContactWithUs";
            return RedirectToAction("Index", "MyAccount");
        }
        [HttpPost]
        public ActionResult ContactWithUs(ContactWithUs cwu)
        {
            if (Request.Cookies["CookieByUserName"] != null)
            {
                try
                {
                    int UID = Convert.ToInt32(Request.Cookies["UserCookieByID"].Value);
                    cwu.UID = UID;
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    cwu = pmb.ContactWithUs(cwu);
                    TempData["ContactWithUsSubmitted"] = "ContactWithUsSubmitted";
                    return RedirectToAction("ContactWithUs");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            TempData["ContactWithUs"] = "ContactWithUs";
            return RedirectToAction("Index", "MyAccount");
        }
    }
}
