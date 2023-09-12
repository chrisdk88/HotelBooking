using Microsoft.EntityFrameworkCore;
using Models;

namespace HotelBooking.Data
{
    public class HotelBookingContext : DbContext
    {
        string conn = @"Data Source=192.168.148.128\MSSQL;Database=AndenGruppeHotelH2;User ID=andengruppe;Password=Password1;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

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
