using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
	public partial class Login
	{
		// Initialize any required resources or services in the constructor if needed.
		public Login()
		{
			// Example: _context = new HotelContext();
		}

		private User LoginUser = new User();

		private async Task HandleLogin()
		{

			if (!string.IsNullOrWhiteSpace(LoginUser.email) && !string.IsNullOrWhiteSpace(LoginUser.password))
			{

				// Replace the hardcoded values with user input.
				string email = LoginUser.email;
				string password = LoginUser.password;

				// Make an HTTP request to authenticate the user.
				Customer user = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");

				if (user != null)
				{
					// Handle a successful login, e.g., navigate to the user's dashboard.
					//works idk why the error the code can still run 
					await JSRuntime.InvokeVoidAsync("eval", "window.location.href = '/';");
				}
				else
				{
					// Invalid credentials. Display an error message.
					string errorMessage = "Invalid credentials. Please check your email and password.";
				}
			}
		}
	}
}
