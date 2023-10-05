using Client.Shared.Utilities;
using Models;
using System.Net.Http.Json;
using System.Text;

namespace Client.Pages
{
    public partial class AdminDashboard
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7285/") };
        public class input 
        {
            public int typeId;
            public int roomNumber;
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
        }
    }
}
