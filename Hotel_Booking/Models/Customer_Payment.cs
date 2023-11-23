using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Booking.Models
{
    public class Customer_Payment
    {


        [ForeignKey("AppUser")]
        public string CustomerID { get; set; }
        public virtual AppUser? AppUser { get; set; }

        [ForeignKey("Payment")]
        public Guid PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
