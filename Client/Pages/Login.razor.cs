using Client.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Login
    {
        private User LoginUser = new User();
        public string errorMessage;
        public Customer? customeruser;
        private async Task HandleLogin()
        {
            if (!string.IsNullOrWhiteSpace(LoginUser.email) && !string.IsNullOrWhiteSpace(LoginUser.password))
            {
                string email = LoginUser.email;
                string password = LoginUser.password;
                // Make an HTTP request to authenticate the user.
                customeruser = await Http.GetFromJsonAsync<Customer>($"https://localhost:7285/api/Customers/{email}/{password}");

                if (customeruser != null)
                {
                    // Handle a successful login, navigate to the users dashboard.
                    NavigationManager.NavigateTo("/");
                    //GlobalAuthState.UserId.Value = customeruser.id;
					GlobalAuthState.UserId = customeruser.id;
                    Console.WriteLine(GlobalAuthState.UserId);
                }
                else
                {
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

        public void Logout()
        {
            customeruser = null; // Clear the authenticated user
            GlobalAuthState.UserId = null;
        }
      
    }
}

