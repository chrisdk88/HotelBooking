
using Microsoft.JSInterop;
using Client.Pages;
using System.Text.Json;
using System.Text;
using Models;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
	public partial class Payment
	{
        private  void ShowAlert()
        {
            JSRuntime.InvokeVoidAsync("alert", "Booking oprettet");
        }
        [Parameter]
        public Booking Value { get; set; } 

        private async Task createbooking()
        {
            var json = JsonSerializer.Serialize(Value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new() { BaseAddress = new Uri("https://localhost:7285/api/") };
            var response = await client.PostAsync("Bookings", content);
        }
    }
}
