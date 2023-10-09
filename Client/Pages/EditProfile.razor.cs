using Microsoft.AspNetCore.Components;
using System.Text;
using Client.Shared.Utilities;
using Models;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace Client.Pages
{
    public partial class EditProfile
    {
        public bool newPass = false;
        private async Task getUserById()
        {
            using (var httpClient = new HttpClient())
            {

                try
                {
 
                    var response = (await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{GlobalAuthState.UserId}"));
                    

                    if (response != null)
                    {
                        customer = response;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7285/") };

        private async Task saveEditProfileChanges()
        {
            if (newPass)
            {
                //hash the password
                var sha = SHA256.Create();
                var passwordBytes = Encoding.Default.GetBytes(customer.password);
                var hashedPasswordBytes = sha.ComputeHash(passwordBytes);
                var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
                customer.password = hashedPassword;
            }
            // Serialize the userModel to JSON
            string json = System.Text.Json.JsonSerializer.Serialize(customer);

            // Create HttpContent with JSON data
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Use HttpClient to send a POST request to your Swagger API
            var response = await client.PutAsync($"api/Customers/{GlobalAuthState.UserId}", content);
        }
    }
}
