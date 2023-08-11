using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignalRWebClient.Models;

namespace SignalRWebClient.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public City city { get; set; }
        public void OnGet()
        {
        }
    }
}
