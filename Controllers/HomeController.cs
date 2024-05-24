using Microsoft.AspNetCore.Mvc;

namespace NETCORE_ALUM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
