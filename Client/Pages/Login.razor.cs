using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Login
    {
        //private readonly HotelContext _context;



        public Login()
        {
            //_context = context;
        }



        private User LoginUser = new User();



        private async Task HandleLogin()
        {
            if (!string.IsNullOrWhiteSpace(LoginUser.email) && !string.IsNullOrWhiteSpace(LoginUser.password))
            {
                // Authenticate the user by checking the email and password.
                Customer user = await Http.GetFromJsonAsync<Customer>("https://localhost:7285/api/Customers/bad/baad");
                Console.WriteLine(user);


                if (user != null)
                {

                    //NavigationManager.NavigateTo("/");
                }
                else
                {
                    // Invalid credentials. Display an error message.
                }
            }
        }
    }
}