using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using HotelBookingFinal.Models;
using UMLHotel;
using HotelBooking.Data;

namespace HotelBookingFinal.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult bookingUpdate()
        {
            HotelBookingContext context = new HotelBookingContext();

            string query = context.Booking.ToList().ToString();

            return RedirectToAction("Index");
        }

    }
}
