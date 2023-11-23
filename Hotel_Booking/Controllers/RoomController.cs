using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{

    [Authorize(Roles = "Customer")]

    public class RoomController : Controller
    {

        private readonly IRoomRepo roomrepo;
        private readonly UserManager<AppUser> userManager;

        public RoomController(IRoomRepo roomrepo, UserManager<AppUser> userManager)
        {
            this.roomrepo = roomrepo;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
           
            var Rooms = roomrepo.GetAllRooms();
            return View(Rooms);
        }

        public async Task<IActionResult> AllhotelRooms(int id, string name)
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.Id = user.Id;
            ViewBag.BranchName = name;
            var Rooms = await roomrepo.GetRoombyHotelId(id);
            return View(Rooms);
        }



    }
}
