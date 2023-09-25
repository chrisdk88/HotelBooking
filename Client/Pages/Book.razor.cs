using Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Client.Shared.Utilities;

namespace Client.Pages
{
    public partial class Book
    {
        public async Task<List<RoomType>?> GetListOfTypes()
        {
            var tempList = await Http.GetFromJsonAsync<List<Room>>("https://localhost:7285/api/Rooms");
            List<RoomType> all = new List<RoomType> {};
            DateTime inputStart = DateTime.Now;
            DateTime inputEnd = DateTime.Now;

            if (tempList != null)
            {
                foreach (var item in tempList)
                {
                    bool isBooked = item.booking != null && isBookedInPeriod(item.booking.startDate, item.booking.endDate, inputStart, inputEnd);
                    if (!isBooked && all.Where(element => element.id == item.typeId).Count() <= 0)
                    {
                        if (item.type != null)
                        {
                            all.Add(item.type);
                        }
                    }
                }
                all.ForEach((a) => Console.WriteLine(a));
            }

            return all;

        }

        public async Task sendRequest()
        {
            var userId = GlobalAuthState.UserId;
            if (userId != null)
            {

                var booking = new Booking()
                {
                    startDate = DateTime.Now,
                    endDate = DateTime.Now,
                    customerid = (uint)userId
                };

                var client = new HttpClient() { BaseAddress = new Uri("https://localhost:7285/api/") };
                var a = JsonSerializer.Serialize(booking);
                var content = new StringContent(a, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Bookings", content);
                
                Console.WriteLine(response.ReasonPhrase);
            }
        }

        private bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }
    }
}