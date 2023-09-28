﻿using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;

using Models;
using System.Net.Http.Json;
using Client.Shared.Utilities;
namespace Client.Pages
{
	public partial class Login
	{
		private User? LoginUser = new User();
		public string errorMessage;
		public Customer? customeruser;
		public Admin? adminuser;
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
						GlobalAuthState.UserId = adminuser.id;
						GlobalAuthState.Name = adminuser.name;
						StateHasChanged();
                        NavigationManager.NavigateTo("/admin");

                        return;
					}
				}
				catch 
				{
					errorMessage = "Invalid credentials. Please check your email and password.";
				}

				try
				{
					// Hash the provided password.
					var sha256 = SHA256.Create();
					var passwordBytes = Encoding.Default.GetBytes(password);
					var hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
					var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
					password = hashedPassword;
					// Admin authentication failed; let's try customer authentication.
					customeruser = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");
				    
					if (customeruser != null)
					{  
						GlobalAuthState.Name = customeruser.name;
						GlobalAuthState.UserId = customeruser.id;
						Console.WriteLine(GlobalAuthState.Name);

                        StateHasChanged();
						NavigationManager.NavigateTo("/");

						return;
					}
				}
				catch 
				{
					errorMessage = "Invalid credentials. Please check your email and password.";
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
				adminuser = null; // Clear the authenticated user
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

