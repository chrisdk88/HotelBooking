using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;
using Client.Shared.Utilities;
using System.Text;
using System.Security.Cryptography;

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
					errorMessage = "Invalid credentials. Please check your email and password.";
				}

				try
				{
					// Admin authentication failed; let's try customer authentication.
					customeruser = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");
					if (customeruser != null)
					{
						// Hash the provided password.
						var sha256 = SHA256.Create();
						var passwordBytes = Encoding.UTF8.GetBytes(password);
						var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
						var hashedPassword = Convert.ToBase64String(hashedPasswordBytes).Replace("-", "").ToLower();
					    Console.WriteLine(hashedPassword);
						Console.WriteLine(customeruser.password);
						if (customeruser.password == hashedPassword)
						{
							NavigationManager.NavigateTo("/");
							GlobalAuthState.UserId = customeruser.id;
							StateHasChanged();
							return;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception: {ex.Message}");
					errorMessage = "Invalid credentials. Please check your email and password222.";
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

