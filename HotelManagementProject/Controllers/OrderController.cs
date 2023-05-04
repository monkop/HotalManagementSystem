using HotelManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagementProject.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public List<OrderDetail> GetAllOrderDetails()
        {
            List<OrderDetail> uList = new List<OrderDetail>();
            try
            {
                HotelManagementDAL h = new HotelManagementDAL();
                uList = h.GetAllOrderDetails();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return uList;

        }
        [HttpGet]
        public OrderDetail GetMenuId(int oid)
        {
            OrderDetail g = null;
            try
            {
                HotelManagementDAL d = new HotelManagementDAL();
                g = d.GetOrderDetailbyId(oid);

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return g;

        }
    }
}
