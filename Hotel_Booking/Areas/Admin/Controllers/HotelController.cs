using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class HotelController : Controller
    {


        private readonly IRoomRepo roomrepo;
        private readonly IHotelRepo hotelRepo;

        private readonly UserManager<AppUser> userManager;
        private readonly ICustomer_RoomRepo customerRoomRepo;


        public HotelController(IRoomRepo roomrepo, IHotelRepo hotelRepo, UserManager<AppUser> userManager, ICustomer_RoomRepo customerRoomRepo)
        {
            this.roomrepo = roomrepo;
            this.userManager = userManager;
            this.customerRoomRepo = customerRoomRepo;
            this.hotelRepo = hotelRepo;
        }


        // GET: HotelController
        public ActionResult Index()
        {
            var Hotels = hotelRepo.GetAllHotels();
            return View(Hotels);
        }

        // GET: HotelController/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelBranch hotel)
        {
            if (ModelState.IsValid)
            {
                hotelRepo.AddHotel(hotel);

                return RedirectToAction(nameof(Index));
            }
            return View(hotel); 
        }

        // GET: HotelController/Edit/5
        public ActionResult Edit(int id)
        {
            var hotel = hotelRepo.GetHotelbyId(id);
            return View(hotel);
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HotelBranch hotel)
        {
           hotelRepo.UpdateHotel(hotel);
            return RedirectToAction(nameof(Index));

        }

        // GET: HotelController/Delete/5
        public ActionResult Delete(int id)
        {

            hotelRepo.DeleteHotel(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
