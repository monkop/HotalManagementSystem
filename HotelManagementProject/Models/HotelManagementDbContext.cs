using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagementProject.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext(): base("name=HotelManagementDBConnectionString")//if we pass db name then it creat the database with that same name
        {
           // Database.SetInitializer<HotelManagementDbContext>(new CreateDatabaseIfNotExists<HotelManagementDbContext>());//when new class is created
            //Database.SetInitializer<HotelManagementDbContext>(new DropCreateDatabaseIfModelChanges<HotelManagementDbContext>());//when changes made in the class stucture
            Database.SetInitializer<HotelManagementDbContext>(new HotelManagementDBInitializer());
        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Room> Rooms { get; set; }

    }
}