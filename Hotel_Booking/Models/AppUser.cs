using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public string? Country { get; set; }

        public string? Phone { get; set; }

        public virtual ICollection<Payment>? Payments { get; set; }

    }
}





