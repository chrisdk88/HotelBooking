using Client.Shared.Utilities;
using Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Client.Pages
{
    public partial class AdminDashboard
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7285/") };
        public List<Room>? rooms;

        public Input input = new()
        {
            inputBooking = new Booking()
            {
                startDate = DateTime.Today.AddHours(12),
                endDate = DateTime.Today.AddHours(12).AddDays(7)
            },
        };
        public class Input 
        {
            public int typeId;
            public int roomNumber;
            
            public Booking inputBooking;
        }

        public async Task GetListOfRooms()
        {
            rooms = await Http.GetFromJsonAsync<List<Room>>("https://localhost:7285/api/Rooms");
        }
        public async Task addRoom()
        {
            Room room = new Room()
            {
                roomNum = i.roomNumber,
                typeId = (uint)i.typeId
            };
            // Serialize the userModel to JSON
            string json = System.Text.Json.JsonSerializer.Serialize(room);
            Console.WriteLine(json);

            // Create HttpContent with JSON data
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Use HttpClient to send a POST request to your Swagger API
            var response = await client.PostAsync("api/Rooms", content);

            await GetListOfRooms();
            StateHasChanged();
        }
        public async Task deleteRoom(uint id)
        {
            // Create a DELETE request to the API to delete the room
            var response = await client.DeleteAsync($"api/Rooms/{id}");

            await GetListOfRooms();
            StateHasChanged();
        }
        public async Task inactiveRoom(uint id)
        {
            uint? UserId = GlobalAuthState.UserId;
            /*****Create booking to post*****/
            Booking booking = new()
            {
                startDate = input.inputBooking.startDate,
                endDate = input.inputBooking.endDate,
                roomId = id,
                customerid = (uint)UserId
            };

            /*****CREATE BOOKING*****/
            var json = JsonSerializer.Serialize(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
            var response = await client.PostAsync("Bookings", content);
        }
    }
}
