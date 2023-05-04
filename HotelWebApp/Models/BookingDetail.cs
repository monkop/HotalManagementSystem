using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Models
{
    public class BookingDetail
    {
        [Key]
        public int BookingDetail_Id { get; set; }

        public string RoomType { get; set; }
        public int Duration { get; set; }
        public float RoomCharges { get; set; }

        public int RoomId { get; set; }
        public virtual BookingDetail rooms { get; set; }
        public string UserID { get; set; }
        public virtual Registration User { get; set; }
    }
}