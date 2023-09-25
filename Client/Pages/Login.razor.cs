using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

namespace Client.Pages
{
	public partial class Login
	{
		private User LoginUser = new User();
		public string errorMessage;
		private int customerId;

        private async Task HandleLogin()
		{
			if (!string.IsNullOrWhiteSpace(LoginUser.email) && !string.IsNullOrWhiteSpace(LoginUser.password))
			{
				string email = LoginUser.email;
				string password = LoginUser.password;

				// Make an HTTP request to authenticate the user.
				Customer Customeruser = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");

				if (Customeruser != null )
				{
					// Handle a successful login, navigate to the users dashboard.
					
					 NavigationManager.NavigateTo("/");
					  customerId = (int)Customeruser.id;
				}
				else
				{
                    Console.WriteLine("abc");
                    // Invalid credentials. Display an error message.
                    errorMessage = "Invalid credentials. Please check your email and password.";
				}
			}
            else
            {
                // Handle empty or invalid input
                errorMessage = "Please enter your email and password.";
            }
        }
	}
}
