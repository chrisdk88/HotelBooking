using Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata;
using System.Diagnostics.Metrics;
using System.Web;

namespace Client.Pages
{
    
    public partial class Book
    {

		public Input input = new()
        {
            inputBooking = new Booking()
            {
                startDate = DateTime.Today.AddHours(12),
                endDate = DateTime.Today.AddHours(12).AddDays(1)
            },
        };
	
		public class Input
        {
            public Booking inputBooking;
            public uint typeId;
        }

        public async Task<List<RoomType>?> GetListOfTypes()
        {
            Console.WriteLine(DateTime.Today.ToShortDateString().Replace("/", "-"));
            Console.WriteLine(HttpUtility.UrlEncode(DateTime.Today.ToShortDateString().Replace("/", "-")));
            var allTypes = await Http.GetFromJsonAsync<List<RoomType>>("https://localhost:7285/api/RoomTypes");

            return allTypes;
        }
        public Booking? booking { get; set; }

        Room availableRoom;
        string bookerrormsg;
		public async Task sendRequest()
        {
            uint? UserId = GlobalAuthState.UserId;
            if (UserId != null)
            {
                try
                {
                    availableRoom = (await Http.GetFromJsonAsync<Room>($"https://localhost:7285/api/Rooms/GetType/{(uint)input.typeId!}"))!;
                } catch {
                    bookerrormsg = "Der er ingen ledige rum!";
                    StateHasChanged();
					return;
                }

                /*****Create booking to post*****/
                booking = new()
                {
                    startDate = input.inputBooking.startDate,
                    endDate = input.inputBooking.endDate,
                    roomId = availableRoom.id,
                    customerid = (uint)UserId
                };
                //  await JsRuntime.InvokeVoidAsync("alert", "Booking oprettet!"); // Alert
                NavigationManager.NavigateTo("/payment");
				StateHasChanged();

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