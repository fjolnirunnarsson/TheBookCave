using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class FootersController : Controller
    {
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }
    
    }
}