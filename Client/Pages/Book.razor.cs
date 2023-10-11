using Models;
using System.Net.Http.Json;
using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Web;
using System.Globalization;

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
			Console.WriteLine(DateTime.Today.ToUniversalTime());
			Console.WriteLine(HttpUtility.UrlEncode("abc"));
			var allTypes = await Http.GetFromJsonAsync<List<RoomType>>("https://localhost:7285/api/RoomTypes");

            return allTypes;
        }
		Room availableRoom;
        Dictionary<uint, string> bookerrormsg = new();
        public Booking booking { get; set; }
        public async Task sendRequest()
        {
            if ((input.inputBooking.endDate - input.inputBooking.startDate).TotalDays < 1)
            {
                await JsRuntime.InvokeVoidAsync("alert", "Datoerne er ikke gyldige!"); // Alert
                return;
            }


            uint? UserId = GlobalAuthState.UserId;
            if (UserId != null)
            {
                try
                {
                    String start = input.inputBooking.startDate.ToString("yyyy-MM-dd HH:mm:ss");
                    String end = input.inputBooking.endDate.ToString("yyyy-MM-dd HH:mm:ss");
					availableRoom = (await Http.GetFromJsonAsync<Room>($"https://localhost:7285/api/Rooms/GetType/{(uint)input.typeId}/{start}/{end}"))!;
                } 
                catch 
                {
                    if (!bookerrormsg.ContainsKey(input.typeId))
                    {
                        bookerrormsg.Add(key: input.typeId, value: "Der er ingen ledige rum!");
                    }
                    StateHasChanged();
					return;
                }
                
                /*****Create booking to post*****/
                Booking booking = new()
                {
                    startDate = input.inputBooking.startDate,
                    endDate = input.inputBooking.endDate,
                    roomId = availableRoom.id,
                    customerid = (uint)UserId
                };

                /*****Save needed data in container*****/
                BookingStateContainer.SetValue(booking);
                BookingStateContainer.SetPrice(availableRoom.type.price);

				NavigationManager.NavigateTo("/payment");
				StateHasChanged();

			} else
            {
                await JsRuntime.InvokeVoidAsync("alert", "Du skal være logget ind!"); // Alert
                NavigationManager.NavigateTo("/login");
            }
        }

		protected override async Task OnInitializedAsync()
		{
            BookingStateContainer.OnStateChange += StateHasChanged;
			types = await GetListOfTypes();
		}

		public bool isBookedInPeriod(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 > end2 && start2 > end1;
        }

        public void Dispose()
        {
			BookingStateContainer.OnStateChange -= StateHasChanged;
        }
 
    }
}