using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HotelManagementProject.Models
{
    public class HotelManagementDBInitializer: DropCreateDatabaseIfModelChanges<HotelManagementDbContext>
    {
        protected override void Seed(HotelManagementDbContext context)
        {
            var Registrations = new List<Registration>
            {
                new Registration{FirstName="Neha",LastName="Patil",Usertype="Admin",UserID="Neha123",Age=23,Gender="Female",Password="Neha123",Address="Pune",Email="nhpatil139@gmail.com",PhoneNo=9175781309},
                new Registration{FirstName="Priya",LastName="Chauhan",Usertype="User",UserID="Priya111",Age=20,Gender="Female",Password="Priya111",Address="Nashik",Email="Priya@gmail.com",PhoneNo=7020724576}
            };
            Registrations.ForEach(g => context.Registrations.Add(g));
            context.SaveChanges();

            var Menus = new List<Menu>
            {
                new Menu{Menu_Type="Veg",Menu_Name="Paneer",Price=100,Quantity=1,Description="No more spices"},
                new Menu{Menu_Type="Non Veg",Menu_Name="Chiken Tikka",Price=250,Quantity=3,Description="More spices"}
            };
            Menus.ForEach(g => context.Menus.Add(g));
            context.SaveChanges();

            //Console.WriteLine("DB created with 2 tables and 2 records added to each table");
            //Console.ReadKey();
        }
    }
}