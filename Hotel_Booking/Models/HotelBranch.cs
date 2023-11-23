using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models
{
    [Table("HotelBranch")]
    public class HotelBranch
    {
        public int ID { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? BranchLocation { get; set; }

        [Required]
        public string PhoneNo { get; set; }

        public virtual ICollection<Room>? Rooms { get; set; }
        public virtual ICollection<AppUser>? AppUsers { get; set; }

    }
}
