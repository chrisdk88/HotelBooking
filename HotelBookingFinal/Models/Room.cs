namespace UMLHotel
{
    public class Room
    {
        public int id { get; set; }
        public bool vacancy { get; set; }
        public String type { get; set; }
        public int price { get; set; }
        public List<Booking> bookings { get; set; }
        void cancelBooking() { }
        void createBooking() { }
    }
}