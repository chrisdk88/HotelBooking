using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Models.RoomType>? RoomType { get; set; }
        public HotelContext(DbContextOptions options) : base(options) { }      
    }
}
