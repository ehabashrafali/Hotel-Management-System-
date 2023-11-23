using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace Hotel_Booking.Repo.Services
{
    public class Customer_PaymentService : ICustomer_PaymentRepo
    {

        public ApplicationDbContext Context { get; set; }

        public Customer_PaymentService(ApplicationDbContext context)
        {
            Context = context;

        }

        public void AddPayment(string userId, Payment payment)
        {
        
            Context.Add(payment);

            Customer_Payment customer_Payment = new Customer_Payment
            {
                PaymentId = payment.ID,
                CustomerID = userId,
            };
            Context.Add(customer_Payment);

            Context.SaveChanges();
      
        }


        public int GetPaymentByUserId(string Userid)
        {
            var customerPayment = Context.Customer_Payments.Select(cp => cp.CustomerID).Distinct().Count();
            return customerPayment;
        }
    }
}
