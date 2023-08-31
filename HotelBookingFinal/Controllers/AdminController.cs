using Microsoft.AspNetCore.Mvc;

namespace HotelBookingFinal.Controllers
{
    public class AdminController : Controller
    {
        //CRUD admin item

        public IActionResult Index()
        {
            return View();
        }
    }
}
