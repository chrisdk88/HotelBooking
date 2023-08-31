using Microsoft.EntityFrameworkCore;
using UMLHotel;

namespace HotelBooking.Data
{
    public class HotelBookingContext : DbContext
    {
        string conn = @"Data Source=PCVDATALAP117\SQLEXPRESS;Database=HotelBooking;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=true;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<Booking> Booking { get; set; } = null;
        public DbSet<Hotel> Hotel { get; set; } = null;
        public DbSet<Room> Room { get; set; } = null;
        public DbSet<Customer> Customer { get; set; } = null;
        public DbSet<Admin> Admin { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conn);
        }

    }
}
