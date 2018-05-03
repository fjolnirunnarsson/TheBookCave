using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;
using System.Linq;

namespace TheBookCave.Controllers
{
    public class EmployeeSiteController : Controller
    {
         private BookService _bookService;


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult EmployeeHome()
        {
            var books = _bookService.GetAllBooks();

            var top10 = (from book in books
                        orderby book.Rating descending
                        select book).ToList();

            return View(top10);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public EmployeeSiteController()
        {
            _bookService = new BookService();
        }

    }

}