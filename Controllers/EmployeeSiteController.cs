using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;
using System.Linq;
using System.Collections.Generic;

namespace TheBookCave.Controllers
{
    public class EmployeeSiteController : Controller
    {
         private BookService _bookService;

        public EmployeeSiteController()
        {
            _bookService = new BookService();
        }
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();

            var booklist = (from book in books
                        select book).ToList();

            return View(booklist);
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Change(int id)
        {

            var books = _bookService.GetAllBooks();

            var onebook = (from b in books
                         where b.Id == id
                         select b).SingleOrDefault();

            return View(onebook);
        }

        [HttpPost]
        public IActionResult Change(BookListViewModel updatedBook)
        {
            using (var db = new DataContext())
            {
                var onebook = (from b in db.Books
                            where b.Id == updatedBook.Id
                            select b).FirstOrDefault();

                onebook.Title = updatedBook.Title;
                onebook.Image = updatedBook.Image;
                onebook.Author = updatedBook.Author;
                onebook.Genre = updatedBook.Genre;
                onebook.Quantity = updatedBook.Quantity;
                onebook.Price = updatedBook.Price;
                onebook.Year = updatedBook.Year;
                onebook.Description = updatedBook.Description;
                onebook.Discount = updatedBook.Discount;
                onebook.DiscountPrice = System.Math.Round((1 - updatedBook.Discount/100) * updatedBook.Price,2);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            // if(id == null) { return View("NotFound") }

            var books = _bookService.GetAllBooks();

            var book = (from b in books
                        where b.Id == id
                        select b).SingleOrDefault();

            // if(student == null) { return View("NotFound"); }

            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(BookInputModel deleteBook)
        {
            using (var db = new DataContext())
            {
                var onebook = (from b in db.Books
                    where b.Id == deleteBook.Id
                    select b).FirstOrDefault();
                db.Books.Remove(onebook);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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

    }
}