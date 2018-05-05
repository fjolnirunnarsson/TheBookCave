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
         private BookService _BookService;

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult EmployeeHome()
        {
            var books = _BookService.GetAllBooks();

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

            SeedDataCreate(book);

            
            return RedirectToAction("EmployeeHome");
            
        }

        [HttpGet]
        public IActionResult Change()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Change(BookInputModel book)
        {
            SeedDataChange(book);

            return RedirectToAction("EmployeeHome");
        }

        
        public EmployeeSiteController()
        {
            _BookService = new BookService();
        }

        public static void SeedDataCreate(BookInputModel book)
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

        public static void SeedDataChange(BookInputModel book)
        {


            using (var db = new DataContext())
            {
                Book Changebook = (from a in db.Books
                                    where a.Id == book.Id
                                    select a).Single();
                Changebook.Title = book.Title; 
                db.SaveChanges();
            }
        }
    }
}