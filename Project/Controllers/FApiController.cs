using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class FApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
