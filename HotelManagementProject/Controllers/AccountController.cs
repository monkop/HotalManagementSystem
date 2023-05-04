using HotelManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelManagementProject.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public List<Registration> GetAllUsers()
        {
            List<Registration> uList = new List<Registration>();
            try
            {
                HotelManagementDAL h = new HotelManagementDAL();
                uList = h.GetAllUsers();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return uList;

        }
        [HttpGet]
        public Registration GetUserId(int uid)
        {
            Registration g = null;
            try
            {
                HotelManagementDAL d = new HotelManagementDAL();
                g = d.GetUserbyId(uid);

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return g;

        }
        
        [HttpPost]
        public int PostUser([FromBody] Registration grd1)
        {
            int result = 0;
            try
            {
                HotelManagementDAL dbDAL = new HotelManagementDAL();
                result = dbDAL.AddUser(grd1);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
        [HttpDelete]
        public int DeleteUser(int UId)
        {
            int result = 0;

            try
            {
                HotelManagementDAL hDAL = new HotelManagementDAL();

                result = hDAL.DeleteUser(UId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
        [HttpPost]
        public int PostUser(int gId, [FromBody] Registration grd)
        {
            int result = 0;
            try
            {
                HotelManagementDAL dbDAL = new HotelManagementDAL();
                grd.Registration_ID = gId;
                result = dbDAL.UpdateUser(grd);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            return result;
        }
        /* [HttpGet]
         public Registration GetGradeId(int gid)
         {
             Registration g = null;
             try
             {

                 HotelManagementDAL d = new HotelManagementDAL();
                 g = d.GetUserbyId(gid);


             }
             catch (Exception ex)
             {
                 //Console.WriteLine(ex.Message);
             }
             return g;

         }*/
    }
}
