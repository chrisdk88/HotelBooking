﻿using Microsoft.AspNetCore.Components;
using Models; 
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text; // Required for Encoding

namespace Client.Pages
{
    public partial class Signup
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7285/") };
        private async Task RegisterUser()
        {
            //hash the password
            var sha = SHA256.Create();
			var passwordBytes = Encoding.Default.GetBytes(customer.password);
			var hashedPasswordBytes = sha.ComputeHash(passwordBytes);
			customer.password = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();

			// Serialize the userModel to JSON
			string json = System.Text.Json.JsonSerializer.Serialize(customer);

            // Create HttpContent with JSON data
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Use HttpClient to send a POST request to your Swagger API
            var response = await client.PostAsync("api/Customers", content);

			
			if (response.IsSuccessStatusCode)
            {
                // Registration successful
                NavigationManager.NavigateTo("/");
            }
            else
            {
                // Registration failed
                NavigationManager.NavigateTo("/signup");
            }
		}
      

    }
}
