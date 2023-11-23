using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repo.Services
{
    public class HotelService : IHotelRepo
    {
        public ApplicationDbContext Context { get; set; }

        public HotelService(ApplicationDbContext context)
        {
            Context = context;

        }

        List<HotelBranch> IHotelRepo.GetAllHotels()
        {
            var hotels = Context.HotelBranches.Include(h => h.Rooms).ToList();

            return hotels;
        }

        HotelBranch IHotelRepo.GetHotelbyId(int id)
        {
            var hotel = Context.HotelBranches.FirstOrDefault(r => r.ID == id);
            return hotel;
        }

        void IHotelRepo.AddHotel(HotelBranch hotel)
        {
            Context.Add(hotel);
            Context.SaveChanges();
        }

        void IHotelRepo.DeleteHotel(int id)
        {
            var hotelBranch = Context.HotelBranches.FirstOrDefault(h => h.ID == id);

            // Delete associated records from Customer_Room table
            var customerRooms = Context.Customer_Rooms.Where(cr => cr.HotelBranchId == id);
            Context.Customer_Rooms.RemoveRange(customerRooms);

            // Remove the hotel itself
            Context.HotelBranches.Remove(hotelBranch);
            Context.SaveChanges();
        }


        public int GetHotelsNumber()
        {
            var hotelsNo = Context.HotelBranches.Count();

            return hotelsNo;
        }

        void IHotelRepo.UpdateHotel(HotelBranch hotel)
        {

            var Hotel = Context.HotelBranches.FirstOrDefault(h => h.ID == hotel.ID);

            if (Hotel != null)
            {
                Hotel.Name = hotel.Name;
                Hotel.PhoneNo = hotel.PhoneNo;
                Hotel.BranchLocation = hotel.BranchLocation;
                Context.SaveChanges();
            }

        }
    }
}
