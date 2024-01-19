using EcomDemo.BAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace EcomDemo.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(AddEditProductModel aepm)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                ProductMasterBAL pmb = new ProductMasterBAL();
                aepm = pmb.GetProduct(aepm);
                return View(aepm);
            }
            else
            {
                return RedirectToAction ("Login", "Account");
            }
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        [HttpPost]
        public ActionResult CreateProduct(HttpPostedFileBase ProductPhoto, AddEditProductModel aepm)
        {
            if (ProductPhoto != null && ProductPhoto.ContentLength>0)
            {
                var image1 = Path.GetFileName(ProductPhoto.FileName);
                var image = Guid.NewGuid() + Path.GetExtension(image1);
                aepm.ProductPhoto = image;
                var path = Path.Combine(Server.MapPath("~/Areas/Image/"), image);
                ProductPhoto.SaveAs(path);
            }
            ProductMasterBAL pmb = new ProductMasterBAL();
            aepm = pmb.CreateProduct(aepm);
            return RedirectToAction("Index", "Product");

        }

        [HttpGet]

        public ActionResult EditProduct(int ID)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                AddEditProductModel aeum = new AddEditProductModel();
                ProductMasterBAL pmb = new ProductMasterBAL();
                aeum = pmb.EditProduct(ID);
                return View(aeum);
            }
           else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(HttpPostedFileBase ProductPhoto, AddEditProductModel aepm)
        {
            if (ProductPhoto != null && ProductPhoto.ContentLength > 0)
            {

                var image1 = Path.GetFileName(ProductPhoto.FileName);
                var image = Guid.NewGuid() + Path.GetExtension(image1);
                aepm.ProductPhoto = image;
                var path = Path.Combine(Server.MapPath("~/Areas/Image/"), image);
                ProductPhoto.SaveAs(path);
            }
            ProductMasterBAL pmb = new ProductMasterBAL();
            pmb.EditProduct(aepm);
            //aepm = pmb.EditProduct(aepm);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult DeleteProduct(int ID)   
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterBAL pmb = new ProductMasterBAL();
            aepm = pmb.DeleteProduct(ID);
           // return View(aepm);
           return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult AddPhoto(int ID)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                AddEditProductModel aepm = new AddEditProductModel();
                ProductMasterBAL pmb = new ProductMasterBAL();
                aepm = pmb.GetProductPhoto(ID);
                Response.Cookies["PID"].Value = ID.ToString();
                // TempData["ProductDetailsPhotoID"] = aepm.PdID;
                return View(aepm);
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        [HttpPost]
        public ActionResult AddPhoto(HttpPostedFileBase[] ProductDetailPhoto, AddEditProductModel aepm)
        {
            foreach(HttpPostedFileBase file in ProductDetailPhoto)
            {
                if(file != null & file.ContentLength>0)
                {
                    var image1 = Path.GetFileName(file.FileName);
                    var image = Guid.NewGuid() + DateTime.Now.ToString("MMdd") + Path.GetExtension(image1);
                    aepm.ProductDetailPhoto = image;
                    var path = Path.Combine(Server.MapPath("~/Areas/Image/"), image);
                    file.SaveAs(path);
                    ProductMasterBAL pmb = new ProductMasterBAL();
                    pmb.AddPhoto(aepm);
                }
                
            }
            TempData["AddPhoto"] = "success";
            // return View(aepm);
            return RedirectToAction("AddPhoto", "Product", new { ID = aepm.ID });
        }
        public ActionResult DeleteProductDetailPhoto(int PdID)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                AddEditProductModel aepm = new AddEditProductModel();
                ProductMasterBAL pmb = new ProductMasterBAL();
                aepm = pmb.DeleteProductDetailPhoto(PdID);
                if (Request.Cookies["PID"] != null)
                {
                    aepm.ID = Convert.ToInt32(Request.Cookies["PID"].Value);
                }
                TempData["DeletePhoto"] = "success";
                return RedirectToAction("AddPhoto", "Product", new { ID = aepm.ID });
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }
}