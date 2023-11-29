using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using EcomDemo.BAL;

namespace EcomDemo.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {
        String ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;

        // GET: Admin/Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                ViewBag.ForgotPasswordSuccess = TempData["PasswordChanged"];
                return View();
            }

        }
        [HttpPost]
        public ActionResult Login(AdminViewModel avm, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                try
                {
                    AdminLoginBAL alb = new AdminLoginBAL();
                    bool result = alb.AdminLogin(avm);
                    //avm = alb.AdminLogin(avm);
                    if (result)
                    {
                        HttpCookie CookieUserName = new HttpCookie("CookieUserName", avm.UserName.ToString());
                        HttpCookie CookieID = new HttpCookie("CookieID", avm.ID.ToString());
                        Response.Cookies.Add(CookieUserName);
                        Response.Cookies.Add(CookieID);
                        // Response.Cookies["UserName"].Value = avm.UserName.ToString();
                        // httpCookie.Expires = DateTime.Now.AddMinutes(1);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        // Response.Write("Login Failed!");
                        ViewBag.LoginError = "";
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }


            else
            {
                try
                {
                    AdminLoginBAL alb = new AdminLoginBAL();
                    bool result = alb.AdminLogin(avm);
                    //avm = alb.AdminLogin(avm);
                    if (result)
                    {
                        HttpCookie CookieUserName = new HttpCookie("CookieUserName", avm.UserName.ToString());
                        HttpCookie CookieID = new HttpCookie("CookieID", avm.ID.ToString());
                        Response.Cookies.Add(CookieUserName);
                        Response.Cookies.Add(CookieID);
                        // This below line will create one more cookie....
                        // Response.Cookies["UserName"].Value = avm.UserName.ToString();
                        // httpCookie.Expires = DateTime.Now.AddMinutes(1);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        // Response.Write("Login Failed!");
                        ViewBag.LoginError = "";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return View();
        }



        public ActionResult LogOut()
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                HttpCookie myCookie = new HttpCookie("CookieUserName");
                myCookie.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(myCookie);
                //Response.Cookies.Clear();
            }
            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cp)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                int aid = Convert.ToInt32(Request.Cookies["CookieID"].Value);
                cp.mycookieID = aid;
                AdminLoginBAL alb = new AdminLoginBAL();
                bool result = alb.ChangePassword(cp);
                if(result)
                {
                    if (ModelState.IsValid)
                    {

                        // TempData["PasswordChanged"] = "PasswordChanged";
                        HttpCookie myCookie = new HttpCookie("CookieUserName");
                        myCookie.Expires = DateTime.Now.AddDays(-1D);
                        Response.Cookies.Add(myCookie);
                        return RedirectToAction("login", "account");
                    }
                }
                else
                {
                    ViewBag.ChangePasswordError = "";
                }
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
            AdminLoginBAL alb = new AdminLoginBAL();
            bool result = alb.GetUserNameFP(fp);
            if (result)
            {
                MailMessage mail = new MailMessage("abhibhardiya@gmail.com", fp.Email);
                mail.Body = "Your New Automated Generated Password is: <b>" + fp.Body + "</b>";
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
                TempData["PasswordChanged"] = "PasswordChanged";
                return RedirectToAction("Login", "Account");
                
            }
            else
            {
                ViewBag.ForgotPasswordError = "";
            }
            return View(fp);
        }
    }


}