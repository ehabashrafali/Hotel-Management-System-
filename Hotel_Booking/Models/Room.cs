using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models
{

    public enum RoomType
    {
        Single,
        Double,
    }

    [Table("Room")]

    public class Room
    {

        public int ID { get; set; }

        [Required]
        public int RoomNo { get; set; }

        [Required]
        public RoomType Type { get; set; }

        public bool Available { get; set; }

        public string? ImagePath { get; set; }

        public int NumberOfBooking { get; set; }

        [Required]
        public string? PricePerNight { get; set; }

        [Required]
        [ForeignKey(nameof(HotelBranch))]
        public int? HotelBranchId { get; set; }
        public virtual HotelBranch? HotelBranch { get; set; }
        public virtual ICollection<AppUser>? AppUsers { get; set; }
    }
}
