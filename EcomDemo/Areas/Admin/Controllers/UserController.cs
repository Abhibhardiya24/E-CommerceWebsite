using EcomDemo.BAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcomDemo.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(AddEditUserModel aeum)
        {
            if(Request.Cookies["CookieUserName"] != null)
            {
                UserMasterBAL umb = new UserMasterBAL();
                aeum = umb.UserGet(aeum);
                return View(aeum);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult Create()
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                return View();
            }
           else
            {
                string returnUrl = Url.Action("Create", "User");
                return RedirectToAction("Login", "Account", new {returnUrl});
            }
        }
        [HttpPost]
        public ActionResult Create(AddEditUserModel aeum)
        {
            UserMasterBAL umb = new UserMasterBAL();
            aeum = umb.UserCreate(aeum);
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult EditUser(int ID)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                AddEditUserModel aeum = new AddEditUserModel();
                UserMasterBAL umb = new UserMasterBAL();
                aeum = umb.UserEditDone(ID);
                return View(aeum);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        [HttpPost]
        public ActionResult EditUser(AddEditUserModel aeum)
        {
            UserMasterBAL umb = new UserMasterBAL();
            aeum = umb.UserEditDone(aeum);
            return RedirectToAction("Index", "User");
        }
        
        public ActionResult DeleteUser(int ID)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                AddEditUserModel aeum = new AddEditUserModel();
                UserMasterBAL umb = new UserMasterBAL();
                aeum = umb.DeleteUser(ID);
                // return View(aeum);
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ContactUs(ContactWithUs cwu)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                try
                {
                    UserMasterBAL umb = new UserMasterBAL();
                    cwu = umb.GetContactUsLists(cwu);
                    return View(cwu);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return RedirectToAction("Login", "Account");
        }


        //public ActionResult ViewMessage(DateTime ContactDate, int ID)
        //{
        //    try
        //    {
        //        ContactWithUs cwu = new ContactWithUs();
        //        cwu.ContactDate = ContactDate;
        //        cwu.UID = ID;
        //        UserMasterBAL umb = new UserMasterBAL();
        //        cwu = umb.ViewMessage(cwu);
        //        return View(cwu);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public ActionResult ViewMessage(int CoID)
        {
            try
            {
                ContactWithUs cwu = new ContactWithUs();
                cwu.CoID = CoID;
                UserMasterBAL umb = new UserMasterBAL();
                cwu = umb.ViewMessage(cwu);
                return View(cwu);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public ActionResult DeleteMessage(DateTime ContactDate, int ID)
        //{
        //    try
        //    {
        //        ContactWithUs cwu = new ContactWithUs();
        //        cwu.ContactDate = ContactDate;
        //        cwu.UID = ID;
        //        UserMasterBAL umb = new UserMasterBAL();
        //        cwu = umb.DeleteMessage(cwu);
        //        TempData["MessageDeleted"] = "MessageDeleted";
        //        return RedirectToAction("ContactUs", "User");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public ActionResult DeleteMessage(int CoID)
        {
            try
            {
                ContactWithUs cwu = new ContactWithUs();
                cwu.CoID = CoID;
                UserMasterBAL umb = new UserMasterBAL();
                cwu = umb.DeleteMessage(cwu);
                TempData["MessageDeleted"] = "MessageDeleted";
                return RedirectToAction("ContactUs", "User");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}