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
        List<Room>? allRooms = new();
        public async Task<List<RoomType>?> GetListOfTypes()
        {
            allRooms = await Http.GetFromJsonAsync<List<Room>>("https://localhost:7285/api/Rooms");
            List<RoomType> allTypes = new List<RoomType> {};
            DateTime inputStart = DateTime.Now;
            DateTime inputEnd = DateTime.Now;

            updateRooms();

            if (allRooms != null)
            {
                foreach (Room item in allRooms)
                {
                    //bool isBooked = item.booking != null && isBookedInPeriod(item.booking.startDate, item.booking.endDate, inputStart, inputEnd);
                    if (
                        //!isBooked && 
                        allTypes.Where(element => element.id == item.typeId).Count() <= 0)
                    {
                        if (item.type != null)
                        {
                            allTypes.Add(item.type);
                        }
                    }
                }
                allTypes.ForEach((a) => Console.WriteLine(a));
            }

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

        private bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }

        private void updateRooms()
        {
            for(int i  = 0; i < allRooms.Count; i++)
            {
                var tempRoom = allRooms[i];
                //for(int index = 0; index < tempRoom.booking.Count; index++)
                //{
                    //var tempBooking = tempRoom.booking[index];
                    //if (tempRoom != null && tempRoom.booking.endDate < DateTime.Now)
                    //{
                        /*****UPDATE ROOM*****/
                        HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
                        String json = JsonSerializer.Serialize(tempRoom);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        Console.WriteLine(content);
                        //var postResponse = await client.PutAsync($"Rooms/{tempRoom.id}", content);
                    //}
                //} 
            }
        }
    }
}