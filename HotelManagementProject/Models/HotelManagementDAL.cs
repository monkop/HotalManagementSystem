using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HotelManagementProject.Models
{
    public class HotelManagementDAL
    {
        public HotelManagementDbContext HotelCtx;

        public HotelManagementDAL()
        {
            HotelCtx = new HotelManagementDbContext();
        }

        public List<Registration> GetAllUsers()
        {
            List<Registration> userlist = new List<Registration>();
            try
            {
                userlist = (from std in HotelCtx.Registrations
                            orderby std.FirstName
                            select std).ToList();
               
                //userlist = HotelCtx.Registrations.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return userlist;
        }
        public Registration GetUserbyId(int uid)
        {
            Registration s1 = null;

            try
            {
                s1 = new Registration();
                s1 = HotelCtx.Registrations
                    .Where(p => p.Registration_ID == uid)
                    .Single();
            }
            catch (Exception ex)
            {
                throw;
            }
            return s1;
        }
        
        public int AddUser(Registration s1)
        {
            int RecsAdded = 0;
            try
            {
                HotelCtx.Registrations.Add(s1);
                RecsAdded = HotelCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return RecsAdded;
        }
        public int DeleteUser(int uid1)
        {
            Registration g1 = null;
            int ResDeleted = 0;
            try
            {
                g1 = HotelCtx.Registrations
                    .Where(g => g.Registration_ID == uid1)
                    .Single<Registration>();
                if (g1 != null)
                {
                    HotelCtx.Entry(g1).State = System.Data.Entity.EntityState.Deleted;
                    ResDeleted = HotelCtx.SaveChanges();
                }
            }
            catch { throw; }
            return ResDeleted;
        }
        public int UpdateUser(Registration std)
        {
            int RecsUpdated = 0;
            Registration s1 = null;

            try
            {
                // s1 = new Grade();
                s1 = HotelCtx.Registrations
                    .Where(p => p.Registration_ID == std.Registration_ID)
                    .Single<Registration>();

                if (s1 != null)
                {
                    s1.Registration_ID = std.Registration_ID;
                    s1.FirstName = std.FirstName;
                    s1.LastName = std.LastName;
                    s1.UserID = std.UserID;
                    s1.Usertype = std.Usertype;
                    s1.Address = std.Address;
                    s1.Age = std.Age;
                    s1.Gender = std.Gender;
                    s1.Password = std.Password;
                    s1.PhoneNo = std.PhoneNo;
                    s1.Email = std.Email;

                    HotelCtx.Entry(s1).State = System.Data.Entity.EntityState.Modified;
                    RecsUpdated = HotelCtx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RecsUpdated;
        }
        public List<Menu> GetAllMenus()
        {
            List<Menu> menulist = new List<Menu>();
            try
            {
                menulist = (from std in HotelCtx.Menus
                      orderby std.Menu_ID
                      select std).ToList();
               // menulist = HotelCtx.Menus.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return menulist;
        }
        public Menu GetMenubyId(int mid)
        {
            Menu s1 = null;

            try
            {
                s1 = new Menu();
                s1 = HotelCtx.Menus
                    .Where(p => p.Menu_ID == mid)
                    .Single();
            }
            catch (Exception ex)
            {
                throw;
            }
            return s1;
        }
        public int AddMenu(Menu m1)
        {
            int RecsAdded = 0;
            try
            {
                HotelCtx.Menus.Add(m1);
                RecsAdded = HotelCtx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return RecsAdded;
        }
        public int DeleteMenu(int mid1)
        {
            Menu g1 = null;
            int ResDeleted = 0;
            try
            {
                g1 = HotelCtx.Menus
                    .Where(g => g.Menu_ID== mid1)
                    .Single<Menu>();
                if (g1 != null)
                {
                    HotelCtx.Entry(g1).State = System.Data.Entity.EntityState.Deleted;
                    ResDeleted = HotelCtx.SaveChanges();
                }
            }
            catch { throw; }
            return ResDeleted;
        }
        
        public int UpdateMenu(Menu m)
        {
            int RecsUpdated = 0;
            Menu s1 = null;

            try
            {
                // s1 = new Grade();
                s1 = HotelCtx.Menus
                    .Where(p => p.Menu_ID == m.Menu_ID)
                    .Single<Menu>();

                if (s1 != null)
                {
                    s1.Menu_ID = m.Menu_ID;
                    s1.Menu_Name = m.Menu_Name;
                    s1.Menu_Type = m.Menu_Type;
                    s1.Price = m.Price;
                    s1.Description =m.Description;
                    

                    HotelCtx.Entry(s1).State = System.Data.Entity.EntityState.Modified;
                    RecsUpdated = HotelCtx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RecsUpdated;
        }
        public List<OrderDetail> GetAllOrderDetails()
        {
            List<OrderDetail> OrderDetaillist = new List<OrderDetail>();
            try
            {

                OrderDetaillist = HotelCtx.OrderDetails.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return OrderDetaillist;
        }
        public OrderDetail GetOrderDetailbyId(int oid)
        {
            OrderDetail s1 = null;

            try
            {
                s1 = new OrderDetail();
                s1 = HotelCtx.OrderDetails
                    .Where(p => p.OrderDetail_Id == oid)
                    .Single();
            }
            catch (Exception ex)
            {
                throw;
            }
            return s1;
        }
       
    }
}