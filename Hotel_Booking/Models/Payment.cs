using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models
{
    public class Payment
    {
        [Key]
        public Guid ID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        public decimal PaymentAmount { get; set; } 

        [ForeignKey("AppUser")]
        public string CustomerID { get; set; }
        public virtual AppUser AppUser { get; set; }
    }

}
