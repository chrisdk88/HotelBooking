namespace Models
{
    public class Booking : Common
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Customer customer { get; set; }
    }
}