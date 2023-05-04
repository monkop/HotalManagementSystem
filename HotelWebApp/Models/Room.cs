using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Models
{
    public class Room
    {
        [Key]
        public int Room_Id { get; set; }
        public string Room_Type { get; set; }
        public int Charges { get; set; }
        public string Description { get; set; }
    }
}