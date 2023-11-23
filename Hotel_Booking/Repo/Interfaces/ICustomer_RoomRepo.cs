using Hotel_Booking.Models;

namespace Hotel_Booking.Repo.Interfaces
{
    public interface ICustomer_RoomRepo
    {
        public int GetCustomersCount();
        bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut);
        bool BookRooms(int roomId, string customerId, int branchId, DateTime checkIn, DateTime checkOut);
        bool CancelBooking(string customerId, int hotelId, int roomId);
        string CalculateDiscountedPrice(bool isPreviousCustomer, string originalPrice);
        public List<Customer_Room> GetbookedRooms(string customerId);
        public Customer_Room Update(int roomId, string customerID, int hotelBranchId, DateTime checkIn, DateTime checkOut);
    }
}

