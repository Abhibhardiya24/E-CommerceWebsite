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
            if(Request.Cookies["UserName"] != null)
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
            if (Request.Cookies["UserName"] != null)
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
            return View(aeum);
        }

        [HttpGet]
        public ActionResult EditUser(int ID)
        {
            if (Request.Cookies["UserName"] != null)
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
            return View(aeum);
        }
        
        public ActionResult DeleteUser(int ID)
        {
            if (Request.Cookies["UserName"] != null)
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
    }
}