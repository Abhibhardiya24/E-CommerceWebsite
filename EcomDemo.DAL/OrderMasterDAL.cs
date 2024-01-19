using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.UI;

namespace EcomDemo.DAL
{
    public class OrderMasterDAL
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["sqlstring"].ConnectionString;
        public OrderList GetOrderList(OrderList ol, int page, int pageSize)
        {
            try
            {
                using (SqlConnection con =new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetOrderReportListsWithPagination"));
                        cmd.Parameters.Add(new SqlParameter("@OffsetValue", (page - 1) * pageSize));
                        cmd.Parameters.Add(new SqlParameter("@PagingSize", pageSize));
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<OrderList> list = new List<OrderList>();
                                while (reader.Read())
                                {
                                    OrderList orderList = new OrderList();
                                    orderList.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    orderList.BillNo = Convert.ToInt32(reader["BillNo"]);
                                    orderList.UID = Convert.ToInt32(reader["U_ID"]);
                                    orderList.UserName = reader["Uname"].ToString();
                                    orderList.Email = reader["Uemail"].ToString();
                                    orderList.MobileNumber = reader["Ucontact"].ToString();
                                    orderList.Address = reader["Uaddress"].ToString();
                                    orderList.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    orderList.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    orderList.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    orderList.Status = Convert.ToInt32(reader["Omstatus"]);
                                    orderList.OrderDate = Convert.ToDateTime(reader["OrderCreateDate"]);
                                    list.Add(orderList);
                                }
                                ol.orderLists = list;   
                            
                            }
                            reader.NextResult();
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    ol.TotalItems = Convert.ToInt32(reader["TotalRecords"]);
                                }
                            }
                        }   
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ol;
        }
        public OrderList GetOrders(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "GetOrderLists"));
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<OrderList> list = new List<OrderList>();
                                while (reader.Read())
                                {
                                    OrderList orderList = new OrderList();
                                    orderList.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    orderList.BillNo = Convert.ToInt32(reader["BillNo"]);
                                    orderList.UID = Convert.ToInt32(reader["U_ID"]);
                                    orderList.UserName = reader["Uname"].ToString();
                                    orderList.Email = reader["Uemail"].ToString();
                                    orderList.MobileNumber = reader["Ucontact"].ToString();
                                    orderList.Address = reader["Uaddress"].ToString();
                                    orderList.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    orderList.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    orderList.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    orderList.Status = Convert.ToInt32(reader["Omstatus"]);
                                    orderList.OrderDate = Convert.ToDateTime(reader["OrderCreateDate"]);
                                    list.Add(orderList);
                                }
                                ol.orderLists = list;

                            }
                           
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ol;
        }
        public OrderList OrderDetails(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "OrderLists"));
                        cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = ol.BillNo;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();  

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                List<OrderList> list = new List<OrderList>();
                                while (reader.Read())
                                {
                                    OrderList orderList = new OrderList();
                                    ol.UID = Convert.ToInt32(reader["U_ID"]);
                                    ol.UserName = reader["Uname"].ToString();
                                    ol.Email = reader["Uemail"].ToString();
                                    ol.MobileNumber = reader["Ucontact"].ToString();
                                    ol.Address = reader["Uaddress"].ToString();
                                    ol.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    ol.TransactionID = reader["TransactionID"].ToString();
                                    ol.SubTotal = Convert.ToDecimal(reader["SubAmount"]);
                                    ol.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    ol.Cgst = Convert.ToDecimal(reader["Cgst"]);
                                    ol.Sgst = Convert.ToDecimal(reader["Sgst"]);
                                    ol.Igst = Convert.ToDecimal(reader["Igst"]);
                                    ol.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    ol.Status = Convert.ToInt32(reader["Omstatus"]);
                                    orderList.PID = Convert.ToInt32(reader["P_ID"]);
                                    orderList.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    orderList.ProductName = reader["Pname"].ToString();
                                    orderList.ProductDescription = reader["Pdecription"].ToString();
                                    orderList.ProductPhoto = reader["Pphoto"].ToString();
                                    orderList.ProductQuantity = Convert.ToInt32(reader["PQty"]);
                                    orderList.ProductPrice = Convert.ToInt32(reader["Pprice"]);
                                    list.Add(orderList);
                                }
                                ol.orderLists = list;
                            }
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ol;
        }

        public OrderList ChangeOrderStatus(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "ChangeOrderStatus"));
                        cmd.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = ol.BillNo;
                        cmd.Parameters.Add(new SqlParameter("@Omstatus", SqlDbType.Int)).Value = ol.Status;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ol;
        }

        public OrderList OrderReport(OrderList ol)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "OrderReport"));
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = ol.StartDate;
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = ol.EndDate;
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                List<OrderList> list = new List<OrderList>();
                                while (reader.Read())
                                {
                                    OrderList orderList = new OrderList();
                                    orderList.SrNo = Convert.ToInt64(reader["SrNo"]);
                                    orderList.BillNo = Convert.ToInt32(reader["BillNo"]);
                                    orderList.UID = Convert.ToInt32(reader["U_ID"]);
                                    orderList.UserName = reader["Uname"].ToString();
                                    orderList.Email = reader["Uemail"].ToString();
                                    orderList.PaymentMode = Convert.ToInt32(reader["PaymentMode"]);
                                    orderList.TotalQty = Convert.ToInt32(reader["TotalQty"]);
                                    orderList.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                    orderList.Status = Convert.ToInt32(reader["Omstatus"]);
                                    orderList.OrderDate = Convert.ToDateTime(reader["Omcreatedate"]);
                                    list.Add(orderList);
                                }
                                ol.orderLists = list;
                            }
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ol;
        }

        public OrderReportChart OrderReportChart(OrderReportChart orc)
        {
            
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderMasterSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@mode", "OrderChartReport"));
                        cmd.CommandTimeout = 0;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                List<OrderReportChart> list = new List<OrderReportChart>();

                                while (reader.Read())
                                {
                                    OrderReportChart chart = new OrderReportChart();
                                    chart.PID = Convert.ToInt32(reader["P_ID"]);
                                    chart.ProductName = reader["Pname"].ToString();
                                    chart.TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"]);
                                    list.Add(chart);
                                }
                                orc.charts = list;
                                
                            }
                            
                        }
                        cmd.Connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return orc;
        }
    }
}
