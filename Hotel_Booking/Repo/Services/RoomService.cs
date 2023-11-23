using Hotel_Booking.Data;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking.Repo.Services
{
    public class RoomService : IRoomRepo
    {
        public ApplicationDbContext Context { get; set; }

        public RoomService(ApplicationDbContext context) 
        {
            Context = context;

        }

        public void AddRoom(Room room)
        {
            Context.Rooms.Add(room);
            Context.SaveChanges();
        }

        public List<Room> GetAllRooms()
        {
            var rooms = Context.Rooms.Include(r => r.HotelBranch).ToList();

            return rooms;
        }

        public Room GetRoombyId(int id)
        {
            var room = Context.Rooms.Include(r => r.HotelBranch).FirstOrDefault(r => r.ID == id);
            return room;
             
        }

        public void DeleteRoom(int id)
        {
            var room = Context.Rooms.FirstOrDefault(r => r.ID == id);


            var customerRooms = Context.Customer_Rooms.Where(cr => cr.RoomId == id);
            Context.Customer_Rooms.RemoveRange(customerRooms);
            Context.Rooms.Remove(room);
            Context.SaveChanges();

        }

        public void UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Room>> GetRoombyHotelId(int id)
        {
            var rooms = await Context.Rooms.Include(r => r.HotelBranch).Where(r => r.HotelBranchId == id).ToListAsync();
            return rooms;
        }

        public int GetRoomsNumber()
        {
            var roomsNo = Context.Rooms.Count();

            return roomsNo;
        }

        public int GetRoomsAvailableNumber()
        {
            var rooms = Context.Rooms.Count(r => r.Available);
            return rooms;
        }
    }
}
