using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
  private BookService _bookService;
        
        public HomeController()
        {
            _bookService = new BookService();
        }
        public IActionResult Index(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(String.IsNullOrEmpty(searchString))
            {
                
                var newestBooks = (from b in books
                                orderby b.BoughtCopies descending
                                select b).Take(8).ToList();
                
                return View(newestBooks);
            }

            var booklist = (from b in books
                        where b.Title.ToLower().Contains(searchString.ToLower())
                        || b.Author.ToLower().Contains(searchString.ToLower())
                        select b).ToList();

            if(booklist.Count == 0)
            {
                return View("NoResults");
            }

            return View(booklist);
        }
        public IActionResult AboutUs(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var booklist = (from b in books
                                where b.Title.ToLower().Contains(searchString.ToLower())
                                || b.Author.ToLower().Contains(searchString.ToLower())
                                select b).ToList();
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                return View("Index", booklist);
            }

            return View();
        }

        public IActionResult TermsAndConditions(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var booklist = (from b in books
                                where b.Title.ToLower().Contains(searchString.ToLower())
                                || b.Author.ToLower().Contains(searchString.ToLower())
                                select b).ToList();
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                return View("Index", booklist);
            }

            return View();
        }

        public IActionResult Help(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var booklist = (from b in books
                                where b.Title.ToLower().Contains(searchString.ToLower())
                                || b.Author.ToLower().Contains(searchString.ToLower())
                                select b).ToList();
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                return View("Index", booklist);
            }

            return View();
        }
    }
}
