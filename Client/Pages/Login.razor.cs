using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Login
    {
        private User LoginUser = new User();
        public string errorMessage;
        private int customerId;
        public Customer customeruser;
        public bool isLoggedIn { get; set; }
        public static class GlobalAuthState
        {
            public static bool IsLoggedIn { get; set; } = false;
            public static int UserId { get; set; } = -1; // Example: You can store the user ID if needed
        }
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
                    isLoggedIn = true;
                    GlobalAuthState.IsLoggedIn = true;
					GlobalAuthState.UserId = (int)customeruser.id;

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
            isLoggedIn = false;
            customeruser = null; // Clear the authenticated user
            GlobalAuthState.IsLoggedIn = false;
            GlobalAuthState.UserId = -1;
        }
    }
}

