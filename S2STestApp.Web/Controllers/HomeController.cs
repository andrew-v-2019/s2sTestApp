
using Microsoft.AspNetCore.Mvc;

namespace S2STestApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
