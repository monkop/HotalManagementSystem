using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetail_Id { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
        public float payment { get; set; }
        public string[] Cart { get; set; }

        public int Menu_Id { get; set; }
        public virtual OrderDetail orders { get; set; }
        public string UserID { get; set; }
        public virtual Registration User { get; set; }
    }
}