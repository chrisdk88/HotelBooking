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
        public Input input = new()
        {
            inputBooking = new Booking()
            {
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddDays(1)
            },
        };

        public class Input
        {
            public Booking inputBooking;
            public uint typeId;
        }

        public async Task<List<RoomType>?> GetListOfTypes()
        {
            var allTypes = await Http.GetFromJsonAsync<List<RoomType>>("https://localhost:7285/api/RoomTypes");

            return allTypes;
        }

        public async Task sendRequest()
        {
            uint? UserId = GlobalAuthState.UserId;
            if (UserId != null)
            {
                //String startString = input.inputBooking.startDate.ToShortDateString().Replace("/", "-");
                //String endString = input.inputBooking.endDate.ToShortDateString().Replace("/", "-");
                //Console.WriteLine(startString.ToString());
                Room availableRoom;
                try
                {
                    availableRoom = (await Http.GetFromJsonAsync<Room>($"https://localhost:7285/api/Rooms/GetType/{(uint)input.typeId!}"))!;
                } catch {
                    await JsRuntime.InvokeVoidAsync("alert", "Der er ingen ledige rum!"); // Alert
                    return;
                }

                var a = (await Http.GetFromJsonAsync<dynamic>($"https://localhost:7285/api/Admins/getFromEmail/k@k.k/k"))!;
                Console.WriteLine(a);
                var b = JsonSerializer.Serialize<Dictionary<Dictionary<String, bool>, Dictionary<String, dynamic>>>(a);
                Console.WriteLine(b);
                // Create booking to post
                //Booking booking = new()
                //{
                //    startDate = input.inputBooking.startDate,
                //    endDate = input.inputBooking.endDate,
                //    roomId = availableRoom.id,
                //    customerid = (uint)UserId
                //};


                ///*****CREATE BOOKING*****/
                //var json = JsonSerializer.Serialize(booking);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                //HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
                //var response = await client.PostAsync("Bookings", content);

                //await JsRuntime.InvokeVoidAsync("alert", "Booking oprettet!"); // Alert
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