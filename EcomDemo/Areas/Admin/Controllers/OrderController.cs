using EcomDemo.BAL;
using EcomDemo.Model;
using System;
using System.Web.Mvc;

namespace EcomDemo.Areas.Admin.Controllers
{
    
    public class OrderController : Controller
    {
       
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
          
            try
            {
                if (Request.Cookies["CookieUserName"] != null)
                {
                    OrderList ol = new OrderList();
                    int pageSize = 10;
                    int currentPage = page ?? 1;
                    OrderMasterBAL omb = new OrderMasterBAL();
                    ol = omb.GetOrderList(ol, currentPage, pageSize);
                    ol = CalculatePagingValues(ol, ol.TotalItems, currentPage, pageSize);

                    return View(ol);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public OrderList CalculatePagingValues(OrderList ol, int totalRecords, int currentPage, int pageSize)
        {
            ol.CurrentPage = currentPage;
            ol.PageSize = pageSize;
            ol.TotalItems = totalRecords;

            // Your logic for calculating other paging-related values
            // Example:
            ol.TotalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);
            ol.StartPage = currentPage - 5;
            ol.EndPage = currentPage + 4;

            // Adjusting start and end page if they go out of bounds
            if (ol.StartPage <= 0)
            {
                ol.EndPage -= (ol.StartPage - 1);
                ol.StartPage = 1;
            }
            if (ol.EndPage > ol.TotalPages)
            {
                ol.EndPage = ol.TotalPages;
                if (ol.EndPage > 10)
                {
                    ol.StartPage = ol.EndPage - 9;
                }
            }

            return ol;
        }

        public ActionResult OrderDetails(int BillNo)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                OrderList ol = new OrderList();
                ol.BillNo = BillNo;
                try
                {
                    OrderMasterBAL omb = new OrderMasterBAL();
                    ol = omb.OrderDetails(ol);
                    return View(ol);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangeOrderStatus(OrderList ol)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                try
                {
                    OrderMasterBAL omb = new OrderMasterBAL();
                    ol = omb.ChangeOrderStatus(ol);
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction("OrderDetails", "Order", new { ol.BillNo });
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]

        public ActionResult OrderReport()
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                OrderList ol = new OrderList();
                OrderMasterBAL omb = new OrderMasterBAL();
                ol = omb.GetOrders(ol);
                TempData["PdfView"] = "PdfView";
                return View(ol);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult OrderReport(OrderList ol)
        {
            if (Request.Cookies["CookieUserName"] != null)
            {
                try
                {
                    // OrderList ol = new OrderList();
                    OrderMasterBAL omb = new OrderMasterBAL();
                    ol = omb.OrderReport(ol);
                    //var pdfResult = new Rotativa.ActionAsPdf("OrderReport", ol);
                    // return pdfResult;

                    return View(ol);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult OrderChart()
        {
            return View();
        }
        public class OrderReportChartResult
        {
            public bool Success { get; set; }
            public OrderReportChart Data { get; set; }
        }

        public JsonResult OrderReportChart()
        {

            OrderReportChart orc = new OrderReportChart();
            OrderMasterBAL omb = new OrderMasterBAL();
            OrderReportChartResult result = new OrderReportChartResult();

            if (Request.Cookies["CookieUserName"] != null)
            {
                result.Data = omb.OrderReportChart(orc);
                result.Success = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.Success = false;
                return Json(new { result, JsonRequestBehavior.AllowGet });
            }
        }
    }
}