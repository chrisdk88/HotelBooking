using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Book
    {
        public async Task<List<Room>?> GetListOfTypes()
        {
            var tempList = await Http.GetFromJsonAsync<Room[]>("https://localhost:7285/api/Rooms");
            List<Room> all = new List<Room> {};
            
            if (tempList != null)
            {
                foreach(var item in tempList)
                {
                    if (all.Where((a) => a.id == 0).ToArray().Length <= 0)
                    {
                        all.Add(item);
                    }
                }
                all.ForEach((a) => Console.WriteLine(a.type));
            }

            return all;

        }

        private bool isBookedInPeriod(DateTime start1, DateTime end1)
        {
            return true;
        }
    }
}