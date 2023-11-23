using Hotel_Booking.Models;

namespace Hotel_Booking.Repo.Interfaces
{
    public interface IRoomRepo
    {
        public int GetRoomsAvailableNumber();
        public int GetRoomsNumber();
        public List<Room> GetAllRooms();
        public Room GetRoombyId(int id);
        Task<List<Room>> GetRoombyHotelId(int id);
        public void AddRoom(Room room);
        public void DeleteRoom(int id);
        public void UpdateRoom(Room room);
    }
}
