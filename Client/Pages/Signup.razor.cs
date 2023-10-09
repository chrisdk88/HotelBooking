using Microsoft.AspNetCore.Components;
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
        string errormsg;
        private async Task RegisterUser()
        {
            var emailExists = await CheckIfEmailExists(customer.email);
            if (emailExists)
            {
                errormsg = "Emailen findes allerede";
                // Email already exists, show an error message
                // You can set a property to display an error message in your component
                // For example: errorMessage = "Email is already in use. Please use a different email.";
                return;
            }
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
        private async Task<bool> CheckIfEmailExists(string email)
        {
            try
            {
                // Send a request to the server to check if the email exists in the database
                var emails = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}");

                if (emails != null)
                {
                    errormsg = "email eksiter";
                    // Email exists in the database
                    return true;
                }
                
                // Email doesn't exist in the database
                return false;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return false;
            }
        }
    }
}
