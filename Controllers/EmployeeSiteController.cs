using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
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

            return View();
        }
        public EmployeeSiteController()
        {
            _bookService = new BookService();
        }

        public static void SeedData(BookInputModel book)
        {
                        using (var db = new DataContext())
            {
                var newBook = new Book { 
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
               
                if(ModelState.IsValid)
                {
                    db.AddRange(newBook);
                    db.SaveChanges();
                    return RedirectToAction("EmployeeHome");
                }
                //Console.WriteLine(blog.BlogId + ": " +  blog.Url);
            }
        }

    }

}
            /*#region Add
            using (var context = new BloggingContext())
            {
                var blog = new Blog { Url = "http://sample.com" };
                context.Blogs.Add(blog);
                context.SaveChanges();

                Console.WriteLine(blog.BlogId + ": " +  blog.Url);
            }
            #endregion*/