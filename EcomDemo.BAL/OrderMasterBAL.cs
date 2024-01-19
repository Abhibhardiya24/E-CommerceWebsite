using EcomDemo.DAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EcomDemo.BAL
{
    public class OrderMasterBAL
    {
        public OrderList GetOrderList(OrderList ol, int page, int pageSize)
        {
            OrderMasterDAL omd = new OrderMasterDAL();
            ol = omd.GetOrderList(ol, page, pageSize);
            return ol;
        }
        public OrderList GetOrders(OrderList ol)
        {
            OrderMasterDAL omd = new OrderMasterDAL();
            ol = omd.GetOrders(ol);
            return ol;
        }
        public OrderList OrderDetails(OrderList ol)
        {
            OrderMasterDAL omd = new OrderMasterDAL();
            ol = omd.OrderDetails(ol);
            return ol;
        }

        public OrderList ChangeOrderStatus(OrderList ol)
        {
            OrderMasterDAL omd = new OrderMasterDAL();
            ol = omd.ChangeOrderStatus(ol);
            return ol;
        }

        public OrderList OrderReport(OrderList ol)
        {
            OrderMasterDAL omd = new OrderMasterDAL();
            ol = omd.OrderReport(ol);
            return ol;
        }

        public OrderReportChart OrderReportChart(OrderReportChart orc)
        {
            
            OrderMasterDAL omd = new OrderMasterDAL();
            orc = omd.OrderReportChart(orc);
            return orc;
        }

    }
}
