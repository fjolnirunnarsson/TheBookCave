using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel book)
        {

            var db = new DataContext();

            var newBook = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Genre = book.Genre,
                Image = book.Image,
                Price = book.Price,
                Discount = book.Discount,
                Quantity = book.Quantity,
                Description = book.Description
            };

            /*if(ModelState.IsValid)
            {
                db.AddRange(newBook);
                db.SaveChanges();
                return RedirectToAction("EmployeeHome");
            }*/

            return View();
        }
    }

}