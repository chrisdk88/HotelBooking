namespace Models
{
    public class Room : Common
    {
        public int roomNum { get; set; }
        public String type { get; set; }
        public int price { get; set; }
        public List<Booking>? booking { get; set; }
        void cancelBooking() { }
        void createBooking() { }
        public Room()
        {
        }
    }
}