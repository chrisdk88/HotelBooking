using Models;
using System.Net.Http.Json;
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
            uint? UserId = GlobalAuthState.UserId;
            if (UserId != null)
            {
                Room availableRoom;
                try
                {
                    availableRoom = (await Http.GetFromJsonAsync<Room>($"https://localhost:7285/api/Rooms/GetType/{(uint)input.typeId!}"))!;
                } catch {
                    await JsRuntime.InvokeVoidAsync("alert", "Der er ingen ledige rum!"); // Alert
                    return;
                }

                // Create booking to post
                Booking booking = new()
                {
                    startDate = input.inputBooking.startDate,
                    endDate = input.inputBooking.endDate,
                    roomId = availableRoom.id,
                    customerid = (uint)UserId
                };


                /*****CREATE BOOKING*****/
                var json = JsonSerializer.Serialize(booking);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
                Console.WriteLine(input.inputBooking.startDate.ToShortDateString);
                Console.WriteLine(input.inputBooking.endDate.ToShortDateString);
                //var response = await client.PostAsync("Bookings", content);

                await JsRuntime.InvokeVoidAsync("alert", "Booking oprettet!"); // Alert
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