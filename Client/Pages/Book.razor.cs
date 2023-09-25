using Models;
using System.Net.Http.Json;
using System.Net.Http;

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

        public void sendRequest()
        {
            string json = "{\"startDate\":\"2023-09-21T08:04:08.714Z\",\"endDate\":\"2023-09-21T08:04:08.714Z\",\"customerid\":1}";

            var tempList = Http.PostAsJsonAsync<String>("https://localhost:7285/api/Bookings", json);
            Console.WriteLine(tempList);
        }

        private bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }
    }
}