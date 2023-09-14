using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class Index
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("abc");


        }
    }
}