using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;
using System.Linq;
using System.Collections.Generic;

namespace TheBookCave.Controllers
{
    public class EmployeeSiteController : Controller
    {
         private BookService _bookService;


        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
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

        [HttpPost]
        public IActionResult Create(BookInputModel book)
        {

            SeedData(book);

            return RedirectToAction("EmployeeHome");
        }

        [HttpGet]
        public IActionResult Change(BookInputModel book)
        {
            return View();
        }
        
        public EmployeeSiteController()
        {
            _bookService = new BookService();
        }

        public static void SeedData(BookInputModel book)
        {
            var db = new DataContext();   
                var Books = new List<Book>
                {
                    new Book{
                        Title = book.Title, 
                        Author = book.Author, 
                        Description = book.Description,
                        Year = book.Year, 
                        Image = book.Image, 
                        Genre = book.Genre,  
                        Price = book.Price, 
                        Discount = book.Discount, 
                        Quantity = book.Quantity, 
                    }
                };
                db.AddRange(Books);
                db.SaveChanges();    
        }
    }
}