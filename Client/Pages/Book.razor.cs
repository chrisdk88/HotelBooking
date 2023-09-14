using Models;
using System.Net.Http.Json;

namespace Client.Pages
{
    public partial class Book
    {
        public async Task<Hotel[]?> Abc()
        {
            return await Http.GetFromJsonAsync<Hotel[]>("https://localhost:7285/api/Rooms");

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine(response);
        }
    }
}