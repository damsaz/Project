using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class NApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
