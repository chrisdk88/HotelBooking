namespace Models
{
    public class User : Common
    {
        public String email { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        //TODO: lave mange til mange mellem hotel og users
        //public List<Hotel> hotels { get; set; }
        void login() { }
    }
}