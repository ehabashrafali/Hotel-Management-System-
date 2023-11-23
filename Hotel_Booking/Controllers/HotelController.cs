using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{

    [Authorize(Roles = "Customer")]

    public class HotelController : Controller
    {

        private readonly IHotelRepo hotelRepo;
        private readonly UserManager<AppUser> userManager;


        public HotelController(IHotelRepo hotelRepo, UserManager<AppUser> userManager)
        {
            this.hotelRepo = hotelRepo;
            this.userManager = userManager;
        }

        // GET: HotelController
        public async Task<ActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.Id = user.Id;
            var hotels = hotelRepo.GetAllHotels();
            return View(hotels);
        }


    }
}
