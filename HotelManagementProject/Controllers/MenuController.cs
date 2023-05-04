using HotelManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagementProject.Controllers
{
    public class MenuController : ApiController
    {
        [HttpGet]
        public List<Menu> GetAllMenus()
        {
            List<Menu> uList = new List<Menu>();
            try
            {
                HotelManagementDAL h = new HotelManagementDAL();
                uList = h.GetAllMenus();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return uList;

        }
        [HttpGet]
        public Menu GetMenuId(int mid)
        {
            Menu g = null;
            try
            {
                HotelManagementDAL d = new HotelManagementDAL();
                g = d.GetMenubyId(mid);

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return g;

        }
        [HttpPost]
        public int PostMenu([FromBody] Menu m)
        {
            int result = 0;
            try
            {
                HotelManagementDAL dbDAL = new HotelManagementDAL();
                result = dbDAL.AddMenu(m);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
        [HttpDelete]
        public int DeleteMenu(int MId)
        {
            int result = 0;

            try
            {
                HotelManagementDAL hDAL = new HotelManagementDAL();

                result = hDAL.DeleteMenu(MId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
        [HttpPost]
        public int PostMenu(int mId, [FromBody] Menu md)
        {
            int result = 0;
            try
            {
                HotelManagementDAL dbDAL = new HotelManagementDAL();
                md.Menu_ID = mId;
                result = dbDAL.UpdateMenu(md);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
