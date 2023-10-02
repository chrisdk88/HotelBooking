using Client.Shared.Utilities;
using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using static Client.Pages.Book;
using System.Text.Json;
using System.Text;

namespace Client.Pages
{
    public partial class Customerview
    {
        public async Task<List<Booking>?> GetUserBookings()
        {
            var userId = GlobalAuthState.UserId;
            if (userId != null)
            {
                var allTypes = await Http.GetFromJsonAsync<List<Booking>>($"https://localhost:7285/api/Bookings/user/{userId}");
                return allTypes;
            }
            return null;
        }


        public async void cancelBooking(uint bookingId)
        {
            /*****CREATE BOOKING*****/

            HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
            var response = await client.DeleteAsync($"Bookings/{bookingId}");
            
            if (response.IsSuccessStatusCode)
            {
                await JsRuntime.InvokeVoidAsync("alert", "Booking annulleret!"); // Alert
            }


            return;
        }
    }
}
