using HotelBooking.Data;
using HotelBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UMLHotel;

namespace HotelBookingFinal.Controllers
{
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HotelBookingContext context = new();

            Hotel hotel1 = new()
            {
                name = "abc",
                phoneNumber = "123",
                address = "abc123",
                rooms = new(),
            };
            context.Hotel.Add(hotel1);
            context.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}