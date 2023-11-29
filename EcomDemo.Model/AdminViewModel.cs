using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace EcomDemo.Model
{
    public class AdminViewModel
    {
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        [Required]
        //[MinLength(10, ErrorMessage = "Minumum 10 characters are required for Password.")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).*$", ErrorMessage ="Password must contain Lowercase, Uppercase, One symbol and Numeric Values.")]
        public string Password { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string CreateDate { get; set; }

    }
    public class ChangePassword
    {
        public int mycookieID { get; set; }
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match with new Password.")]
        public string ConfirmNewPassword { get; set; }
    }
    public class ForgotPassword
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string To { get; set; }  
        public string From { get; set; }
        public string Body { get; set; }


    }
    public class AddEditUserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string CreateDate { get; set; }

        public List<AdminViewModel> adminViewModel { get; set; }

    }

    public class ProductViewModel
    {
        public int ID { get; set; }
        public int PdID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDetailPhoto { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public string CreateDate { get; set; }
    }
    public class AddEditProductModel
    {
        public int ID { get; set; }
        public int PdID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDetailPhoto { get; set; }
        //public HttpPostedFileBase[] ProductDetailPhoto { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public string CreateDate { get; set; }

        public List<ProductViewModel> productViewModel { get; set; }


    }
}
