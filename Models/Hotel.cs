namespace Models
{
    public class Hotel : Common
    {
        public String name { get; set; }
        public String phoneNumber { get; set; }
        public String address { get; set; }
        public List<Admin> admins { get; set; }
        public List<Customer> customers { get; set; }
        public List<Room> rooms { get; set; }
        //void createRoom(String roomType)
        //{
        //    HotelBookingContext HotelBookingCTX = new();
        //    int price = roomType == "Standard" ? 1000 : roomType == "Premium" ? 2000 : 3000;

        //    Room newRoom = new()
        //    {
        //        vacancy = true,
        //        type = roomType,
        //        price = price,
        //        booking = null,
        //    };

        //    HotelBookingCTX.Room.Add(newRoom);
        //    HotelBookingCTX.SaveChanges();

        //}

        //void deleteRoom(int roomNum)
        //{
        //    HotelBookingContext HotelBookingCTX = new();

        //    var tempRoom = HotelBookingCTX.Room.Where(room => room.roomNum == roomNum).First();
        //    HotelBookingCTX.Room.Remove(tempRoom);

        //    HotelBookingCTX.SaveChanges();
        //}
        void editRoom() { }    
    }
}