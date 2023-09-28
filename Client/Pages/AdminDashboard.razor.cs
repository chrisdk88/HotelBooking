using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class AdminDashboard
    {
        public async Task<List<Room>?> GetListOfRooms()
        {
            var allRooms = await Http.GetFromJsonAsync<List<Room>>("https://localhost:7285/api/Rooms");
            foreach (var item in allRooms)
            {
                Console.WriteLine(item);
            }
            return allRooms;
        }
    }
}
