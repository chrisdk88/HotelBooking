using Models;

namespace Client.Pages
{
    public partial class Login
    {
		private User user = new User();

		protected async Task handleLogin(bool firstRender)
		{
			//skal nok ændres til admin og customer
			string email = user.email;
			string password = user.password;
			

		}
	}
}
