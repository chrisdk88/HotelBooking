
using Microsoft.JSInterop;
using Models;
using System.Text.Json;
using System.Text;


namespace Client.Pages
{
	public partial class Payment
	{
        private Booking? booking = null;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            try
            {
                booking = BookingStateContainer.Value;
            } catch
            {
                Console.WriteLine("error");
            }
        }
        
        private async void createBooking()
        {
            /*****CREATE BOOKING*****/
            var json = JsonSerializer.Serialize(booking);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
            var response = await client.PostAsync("Bookings", content);

            await JSRuntime.InvokeVoidAsync("alert", "Booking oprettet!"); // Alert
            NavigationManager.NavigateTo("/");
        }
    }
}
