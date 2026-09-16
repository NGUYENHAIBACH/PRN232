using Microsoft.AspNetCore.Mvc;

namespace WebApiClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
