using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class TermsAndConditionsController: Controller
    {
                [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}