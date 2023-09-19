namespace Models
{
    public class Customer : User
    {
        public int age { get; set; }
        public int phoneNumber { get; set; }
        public String address { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
    }
}