using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HotelBooking.Data;
using HotelBookingFinal.Models;
using UMLHotel;
//login
namespace HotelBookingFinal.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Login(string email, string password)
        {
            using (var context = new HotelBookingContext())
            {
                var admin = context.Admin.FirstOrDefault(a => a.email == email && a.password == password);

                var customer = context.Customer.FirstOrDefault(c => c.email == email && c.password == password);

                if (admin != null || customer != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View();
                }
            }
        }
        public IActionResult CreateLogin()
        {
            return View();
        }
    }
}
