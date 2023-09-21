using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Book
    {
        public async Task<List<RoomType>?> GetListOfTypes()
        {
            var tempList = await Http.GetFromJsonAsync<Room[]>("https://localhost:7285/api/RoomTypes");
            List<RoomType> all = new List<RoomType> {};
            DateTime inputStart = DateTime.Now;
            DateTime inputEnd = DateTime.Now;

            if (tempList != null)
            {
                    Console.WriteLine(tempList.First().typeId);
                foreach (var item in tempList)
                {
                    Console.WriteLine(item.typeId);
                    bool isBooked = item.booking != null && isBookedInPeriod(item.booking.startDate, item.booking.endDate, inputStart, inputEnd);
                    if (all.Where((a) => !isBooked).ToArray().Length <= 0)
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

        private bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }
    }
}