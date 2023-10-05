
using Microsoft.JSInterop;

namespace Client.Pages
{
	public partial class Payment
	{
        //  await JsRuntime.InvokeVoidAsync("alert", "Booking oprettet!"); // Alert
        private  void ShowAlert()
        {
            // Display an alert when the div is clicked
            JSRuntime.InvokeVoidAsync("alert", "Booking oprettet");
        }
    }
}
