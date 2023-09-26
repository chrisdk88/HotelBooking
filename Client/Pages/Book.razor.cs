using Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public partial class Book
    {
        public async Task<List<RoomType>?> GetListOfTypes()
        {
            var allTypes = await Http.GetFromJsonAsync<List<RoomType>>("https://localhost:7285/api/RoomTypes");
            allTypes.ForEach((a) => Console.WriteLine(a));

            return allTypes;
        }

        public async Task sendRequest()
        {
            var userId = GlobalAuthState.UserId;
            if (userId != null)
            {
                Booking booking = new()
                {
                    startDate = DateTime.Now,
                    endDate = DateTime.Now.AddDays(1),
                    customerid = (uint)userId
                };

                HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
                /*****CREATE BOOKING*****/
                String json = JsonSerializer.Serialize(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine(content);
                var postResponse = await client.PostAsync("Bookings", content);
                /*****PUT BOOKING IN ROOM*****/
                //Room? avaliableRoom = allRooms.Where(room => !isBookedInPeriod(booking.startDate, booking.endDate, room.booking.startDate, room.booking.endDate)).First();
                //if (avaliableRoom != null)
                //{
                //    json = JsonSerializer.Serialize(avaliableRoom);
                //    Console.WriteLine(json);
                //}
                //var putResponse = await client.PutAsync("bookings", content);


                Console.WriteLine(postResponse.ReasonPhrase);
            } else
            {
                await JsRuntime.InvokeVoidAsync("alert", "Du skal være logget ind!"); // Alert
                NavigationManager.NavigateTo("/login");
            }
        }

        public bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }
    }
}