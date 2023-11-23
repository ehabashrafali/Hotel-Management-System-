using Hotel_Booking.Models;

namespace Hotel_Booking.Repo.Interfaces
{
    public interface ICustomer_PaymentRepo
    {

        public void AddPayment (string UserId, Payment payment);
        public int GetPaymentByUserId (string Userid);
    }
}
