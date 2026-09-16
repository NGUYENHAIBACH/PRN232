using Microsoft.AspNetCore.Mvc;

namespace WebAPIClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
