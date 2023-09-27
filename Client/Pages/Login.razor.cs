using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;
using Client.Shared.Utilities;
namespace Client.Pages
{
	public partial class Login
	{
		private User LoginUser = new User();
		public string errorMessage;
		public Customer customeruser;
		public Admin adminuser;
		private async Task HandleLogin()
		{
			if (!string.IsNullOrWhiteSpace(LoginUser.email) && !string.IsNullOrWhiteSpace(LoginUser.password))
			{
				string email = LoginUser.email;
				string password = LoginUser.password;
				try
				{
					// Make an HTTP request to authenticate the admin.
					adminuser = await Http.GetFromJsonAsync<Admin>($"https://localhost:7285/api/Admins/{email}/{password}");
					if (adminuser != null)
					{
						NavigationManager.NavigateTo("/");
						GlobalAuthState.UserId = adminuser.id;
						Console.WriteLine(adminuser);

						StateHasChanged();
						return;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception: {ex.Message}");
					errorMessage = "invalid email or username";
				}

				try
				{
					// Admin authentication failed; let's try customer authentication.
					customeruser = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");
					if (customeruser != null)
					{
						NavigationManager.NavigateTo("/");
						GlobalAuthState.UserId = customeruser.id;
						StateHasChanged();
						Console.WriteLine(customeruser);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception: {ex.Message}");
					errorMessage = "invalid email or username";
				}
			}

			else
			{
				// Handle empty or invalid input
				errorMessage = "Please enter your email and password.";
			}
		}

		public void Logout()
		{
			try
			{
				customeruser = null; // Clear the authenticated user
				GlobalAuthState.UserId = null;
				StateHasChanged();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception: {ex.Message}");
			}
		}
	}
}

