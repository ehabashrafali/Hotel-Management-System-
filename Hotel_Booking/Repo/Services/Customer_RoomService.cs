using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repo.Services
{
    public class Customer_RoomService : ICustomer_RoomRepo
    {
        public ApplicationDbContext Context { get; set; }

        public Customer_RoomService(ApplicationDbContext context)
        {
            Context = context;

        }

        public bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var bookings = Context.Customer_Rooms.Where(b =>
                            b.CheckIn < checkOut && b.CheckOut > checkIn && b.HotelBranch.Rooms.Any(r => r.ID == roomId));

            return bookings.Any() == false;
        }

        public bool BookRooms(int roomId, string customerId, int branchId, DateTime checkIn, DateTime checkOut)
        {
          
                if (!IsRoomAvailable(roomId, checkIn, checkOut))
                {
                    return false;
                }

            var booking = new Customer_Room
            {
                CustomerID = customerId,
                HotelBranchId = branchId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut
            };

            Context.Customer_Rooms.Add(booking);

                var room = Context.Rooms.FirstOrDefault(r => r.ID == roomId);
                if (room != null)
                {
                    room.NumberOfBooking++;
                    if (room.Type == RoomType.Single && room.NumberOfBooking == 1)
                    {
                        room.Available = false;
                    }
                    else if (room.Type == RoomType.Double && room.NumberOfBooking == 2)
                    {
                        room.Available = false;
                    }
                }

            Context.SaveChanges();
            return true;
        }

        public bool CancelBooking(string customerId, int hotelId, int roomId)
        {
            var booking = Context.Customer_Rooms.FirstOrDefault(b => b.AppUser.Id == customerId && b.HotelBranchId == hotelId && b.Room.ID == roomId);


            Context.Customer_Rooms.Remove(booking);

            var rooms = Context.Rooms.Where(r => r.HotelBranchId == hotelId).ToList();
            foreach (var room in rooms)
            {
                if (room.NumberOfBooking <= 0)
                {
                    room.NumberOfBooking = 0;
                    room.Available = true;
                   
                }
                else
                {
                    room.NumberOfBooking--;

                    if (room.Type == RoomType.Single && room.NumberOfBooking == 0)
                    {
                        room.Available = true;
                    }
                    else if (room.Type == RoomType.Double && room.NumberOfBooking < 2)
                    {
                        room.Available = true;
                    }

                }
            }
            Context.SaveChanges();
            return true;
        }

        public List<Customer_Room> GetbookedRooms(string customerId)
        {
            var rooms = Context.Customer_Rooms.Include(r => r.HotelBranch)
                .ThenInclude(r => r.Rooms).Include(r => r.AppUser)
                .Where(c => c.CustomerID == customerId).ToList();
            return rooms;

        }

        public string CalculateDiscountedPrice(bool isPreviousCustomer, string originalPriceString)
        {
            if (!decimal.TryParse(originalPriceString, out decimal originalPrice))
            {
                throw new ArgumentException("Invalid original price.");
            }

            decimal discountedPrice = isPreviousCustomer ? originalPrice * 0.95m : originalPrice;
            return discountedPrice.ToString();
        }
        public Customer_Room Update(int roomId, string customerId, int branchId, DateTime checkIn, DateTime checkOut)
        {
            var booking = Context.Customer_Rooms.FirstOrDefault(b => b.CustomerID == customerId && b.HotelBranchId == branchId && b.RoomId == roomId);

            if (booking != null)
            {
                booking.CheckIn = checkIn;
                booking.CheckOut = checkOut;

                Context.Customer_Rooms.Update(booking);
                Context.SaveChanges();
            }

            return booking;
        }

        public int GetCustomersCount()
        {
            int customers = Context.Customer_Rooms.Select(b => b.CustomerID).Distinct().Count();
            return customers;
        }
    }
}
