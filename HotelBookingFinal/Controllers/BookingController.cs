using Microsoft.AspNetCore.Mvc;

namespace HotelBookingFinal.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
