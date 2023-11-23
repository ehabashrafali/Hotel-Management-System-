using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Booking.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoomController : Controller
    {

        private readonly IRoomRepo roomrepo;
        private readonly IHotelRepo hotelRepo;

        private readonly UserManager<AppUser> userManager;
        private readonly ICustomer_RoomRepo customerRoomRepo;



        public RoomController(IRoomRepo roomrepo, IHotelRepo hotelRepo, UserManager<AppUser> userManager, ICustomer_RoomRepo customerRoomRepo)
        {
            this.roomrepo = roomrepo;
            this.userManager = userManager;
            this.customerRoomRepo = customerRoomRepo;
            this.hotelRepo = hotelRepo;
        }

        // GET: RoomController
        public ActionResult Index()
        {
            var Rooms = roomrepo.GetAllRooms();
            return View(Rooms);
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            var hotelBranches = hotelRepo.GetAllHotels();
            ViewBag.HotelBranches = new SelectList(hotelBranches, "ID", "Name");
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                roomrepo.AddRoom(room);

                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            var room = roomrepo.GetRoombyId(id);
            var hotelBranches = hotelRepo.GetAllHotels();
            ViewBag.HotelBranches = new SelectList(hotelBranches, "ID", "Name");
            return View(room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            roomrepo.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
