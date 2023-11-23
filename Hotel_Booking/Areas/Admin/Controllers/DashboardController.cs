using Hotel_Booking.Areas.Admin.ViewModel;
using Hotel_Booking.Models;
using Hotel_Booking.Repo.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Hotel_Booking.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashboardController : Controller
    {

        private readonly IRoomRepo roomrepo;
        private readonly IHotelRepo hotelRepo;

        private readonly UserManager<AppUser> userManager;
        private readonly ICustomer_RoomRepo customerRoomRepo;


        public DashboardController(IRoomRepo roomrepo, IHotelRepo hotelRepo, UserManager<AppUser> userManager, ICustomer_RoomRepo customerRoomRepo)
        {
            this.roomrepo = roomrepo;
            this.userManager = userManager;
            this.customerRoomRepo = customerRoomRepo;
            this.hotelRepo = hotelRepo;
        }

        [Area("Admin")]
        // GET: TestController
        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("Customer"))
                {
                    return LocalRedirect(Url.Action("Index", "Hotel"));

                }
                else
                {
                     
                    HomePageViewModel hmPageViewModel = new HomePageViewModel()
                    {
                        HotelsNumber = hotelRepo.GetHotelsNumber(),
                        RoomsNumber = roomrepo.GetRoomsNumber(),
                        CustomersCount = customerRoomRepo.GetCustomersCount(),
                        AvailableRooms = roomrepo.GetRoomsAvailableNumber(),
             
                    };
                    var user = userManager.GetUserAsync(User).Result; // Get the current user

                    if (user != null)
                    {
                        // You can pass user-related data to the view here
                        ViewData["FullName"] = user.FullName;
                    }

                    return View(hmPageViewModel);
                }
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
   
        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: TestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestController/Edit/5
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

        // GET: TestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
