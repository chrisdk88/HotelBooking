namespace UMLHotel
{
    public class Room
    {
        public int id { get; set; }
        public int roomNum { get; set; }
        public bool vacancy { get; set; }
        public String type { get; set; }
        public int price { get; set; }
        public Booking? booking { get; set; }
        void cancelBooking() { }
        void createBooking() { }
    }
}