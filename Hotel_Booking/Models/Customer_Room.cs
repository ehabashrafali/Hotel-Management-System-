using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models
{
    public class Customer_Room
    {

        [ForeignKey(nameof(AppUser))]
        public string CustomerID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        [ForeignKey(nameof(HotelBranch))]
        public int? HotelBranchId { get; set; }
        public virtual HotelBranch HotelBranch { get; set; }

        [ForeignKey(nameof(Room))]
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-In")]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check-Out")]
        public DateTime CheckOut { get; set; }
    }

}
