using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class EmployeeSiteController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult EmployeeHome()
        {
            return View();
        }
    }

}