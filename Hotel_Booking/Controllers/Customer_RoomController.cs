using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Hotel_Booking.Repo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{

    [Authorize(Roles = "Customer")]

    public class Customer_RoomController : Controller
    {

        private readonly IRoomRepo roomrepo;
        private readonly ICustomer_RoomRepo customerRoomRepo;
        private readonly ICustomer_PaymentRepo customer_PaymentRepo;
        private readonly UserManager<AppUser> userManager;

        public Customer_RoomController(IRoomRepo roomrepo, UserManager<AppUser> userManager, ICustomer_RoomRepo customerRoomRepo, ICustomer_PaymentRepo customer_PaymentRepo)
        {
            this.roomrepo = roomrepo;
            this.userManager = userManager;
            this.customerRoomRepo = customerRoomRepo;
            this.customer_PaymentRepo = customer_PaymentRepo;
        }

        [HttpGet]
        public IActionResult Book(int id, int hotelId, string userId)
        {
            ViewBag.UserId = userId;
            ViewBag.hotelId = hotelId;
            var room = roomrepo.GetRoombyId(id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Book(int roomId, string userId, int hotelId, DateTime checkIn, DateTime checkOut)
        {
            var isBooked = customerRoomRepo.BookRooms(roomId, userId, hotelId, checkIn, checkOut);

            if (isBooked)
            {
                return RedirectToAction("AllhotelRooms", "Room", new { id = roomId });

            }
            else
            {
                var room = roomrepo.GetRoombyId(roomId);
                return View("Book", room);
            }

        }

        public IActionResult ViewBookings(string userId)
        {
            var paymentsCount = customer_PaymentRepo.GetPaymentByUserId(userId);
            ViewData["PaymentsCount"] = paymentsCount;

            ViewBag.UserId = userId;
            var bookings = customerRoomRepo.GetbookedRooms(userId);

            bool hasBookings = bookings.Count > 0;
            ViewData["HasBookings"] = hasBookings;
            return View(bookings);
        }

        public IActionResult DeleteBooking(string customerId, int hotelId, int roomId)
        {
            var isCanceled = customerRoomRepo.CancelBooking(customerId, hotelId, roomId);

            return RedirectToAction("ViewBookings", new { userId = customerId });

        }

        public IActionResult Edit(string customerId, int hotelId, int roomId)
        {
            var booking = customerRoomRepo.GetbookedRooms(customerId).FirstOrDefault(r => r.HotelBranchId == hotelId && r.RoomId == roomId);
            ViewBag.Id = customerId;
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Customer_Room booking)
        {
            var Customer_Room = customerRoomRepo.Update(booking.RoomId.Value, booking.CustomerID, booking.HotelBranchId.Value, booking.CheckIn, booking.CheckOut);

            return RedirectToAction("ViewBookings", new { userId = booking.CustomerID }); 

        }

        public IActionResult Confirm(string UserId, Payment payment)
        {

            // Access the PaymentDate from the form
            var paymentDate = payment.PaymentDate;

            // Create a new Payment object with the provided PaymentDate
            var newPayment = new Payment
            {
                PaymentDate = paymentDate,
                PaymentAmount = payment.PaymentAmount,
                CustomerID = UserId
            };

            customer_PaymentRepo.AddPayment(UserId, newPayment);

            return RedirectToAction("Index", "Hotel");
        }


    }
}
