using Hotel_Booking.Models;

namespace Hotel_Booking.Repo.Interfaces
{
    public interface IHotelRepo
    {

        public int GetHotelsNumber();
        public List<HotelBranch> GetAllHotels();
        public HotelBranch GetHotelbyId(int id);
        public void AddHotel(HotelBranch hotel);
        public void DeleteHotel(int id);
        public void UpdateHotel(HotelBranch hotel);


    }
}
