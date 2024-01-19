using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace EcomDemo.Model
{
    public class AdminViewModel
    {
        public Int64 SrNo { get; set; }
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
        public string UserName { get; set; }
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
        [Required]
        public string UserName { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        // [RegularExpression (@"^\d{10}$", ErrorMessage ="Mobile Number must be 10 Digits and ")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter only Numeric Values.")]
        [MinLength(10, ErrorMessage = "Minimum of 10 characters required.")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters exceeded.")]
        public string MobileNumber { get; set; }
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
        public Int64 SrNo { get; set; }
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

    public class UserLogin
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
       
        [Required(ErrorMessage = "Mobile Number is required")]
        // [RegularExpression (@"^\d{10}$", ErrorMessage ="Mobile Number must be 10 Digits and ")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter only Numeric Values.")]
        [MinLength(10, ErrorMessage = "Minimum of 10 characters required.")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters exceeded.")]
        public string MobileNumber { get; set; }
        //public string Body { get; set; }

    }

    public class UserShop
    {
        public int PID { get; set; }
        public int PMID { get; set; }
        public int PDID { get; set; }
        public int UID { get; set; }
        public int WLCount { get; set; }
        public int CmQty { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
        public string ProductDetailPhoto { get; set; }
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }

        public List<UserShop> userShopList { get; set; }
    }

    public class WishList
    {
        public int WLID { get; set; }
        public int UID { get; set; }
        public int PID { get; set;}
        public int CMQty { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductPrice { get; set; }
        public List<WishList> wishList { get; set; }

    }
    public class MyCart
    {
        public int PID { get; set; }
        public int UID { get; set; }
        public int CMID { get; set; }
        public int CMQty { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductPrice { get; set; }
        public int SubTotal { get; set; }
        public string ProductName { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal TotalAmount { get; set; }

        public List<MyCart> myCartList { get; set; }
    }
    public class CheckOut
    {
        public int PID { get; set; }
        public int UID { get; set; }
        public int CMQty { get; set; }
        public int BillNo { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int PaymentMode { get; set; }
        public string TransactionID { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductPrice { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQty { get; set; }
        public decimal SubTotal { get; set; }
        public string ProductName { get; set; }
        public string Productdescription { get; set; }
        public int ProductQuantity { get; set; }
        public int OneProductTotalAmount { get; set; }
        public List<CheckOut> checkOutList { get; set; }
        public List<CheckOut> productLists { get; set; }
       
    }

    public class OrderList
    {
        public Int64 SrNo { get; set; }
        public int BillNo { get; set; }
        public int PID { get; set; }
        public int UID { get; set; }
        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int PaymentMode { get; set; }
        public string TransactionID { get; set; }
        public decimal SubTotal { get; set; }
        public int TotalQty { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal Igst { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<OrderList> orderLists { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

    }
    public class Pager
    {
        //public Pager(int totalItems, int? page, int pageSize = 10)
        //{
        //    // Total Paging need to show
        //    int _totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
        //    //Current Page
        //    int _currentPage = page != null ? (int)page : 1;
        //    //Paging to be starts with
        //    int _startPage = _currentPage - 5;
        //    //Paging to be end with
        //    int _endPage = _currentPage + 4;
        //    if (_startPage <= 0)
        //    {
        //        _endPage -= (_startPage - 1);
        //        _startPage = 1;
        //    }
        //    if (_endPage > _totalPages)
        //    {
        //        _endPage = _totalPages;
        //        if (_endPage > 10)
        //        {
        //            _startPage = _endPage - 9;
        //        }
        //    }
        //    //Setting up the properties
        //    TotalItems = totalItems;
        //    CurrentPage = _currentPage;
        //    PageSize = pageSize;
        //    TotalPages = _totalPages;
        //    StartPage = _startPage;
        //    EndPage = _endPage;
        //}

        
    }

    //public class PersonViewModel
    //{
    //    public Pager Pager { get; set; }
    //}

    public class ContactWithUs
    {
        public Int64 SrNo { get; set; }
        public int CoID { get; set; }
        public int UID { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime ContactDate { get; set; }
        public List<ContactWithUs> contactLists { get; set; }

    }
    public class OrderReportChart
    {
        public int PID { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantitySold { get; set; }
        public List<OrderReportChart> charts { get; set; }
    }


}
