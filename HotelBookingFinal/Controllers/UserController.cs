using Microsoft.AspNetCore.Mvc;
//login
namespace HotelBookingFinal.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
