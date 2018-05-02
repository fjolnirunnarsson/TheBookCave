using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class AboutUSController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
    
}