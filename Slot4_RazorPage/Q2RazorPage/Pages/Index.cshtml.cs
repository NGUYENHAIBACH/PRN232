using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Q2RazorPage.Pages
{
    public class RedirectToTicketModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/Ticket");
        }
    }
}
