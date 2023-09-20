using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;


namespace Client.Pages
{
	public partial class Login
	{
	
		private User LoginUser = new User();
		public string errorMessage;
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
				
					// Handle a successful login, navigate to the users dashboard.
					
					 NavigationManager.NavigateTo("/");

				}
				else
				{
					// Invalid credentials. Display an error message.
					 errorMessage = "Invalid credentials. Please check your email and password.";
				}
			}
		}
	}
}
