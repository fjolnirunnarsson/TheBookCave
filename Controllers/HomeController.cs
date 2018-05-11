using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;
using TheBookCave.Models.ViewModels;
using System.Dynamic;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;
        private WishListService _wishListService;
        
        public HomeController()
        {
            _bookService = new BookService();
            _wishListService = new WishListService();
        }
        public IActionResult Index(string searchString)
        {
            dynamic mymodel = new ExpandoObject();

            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            var books = _bookService.GetAllBooks();

            if(String.IsNullOrEmpty(searchString))
            {
                
                var newestBooks = (from b in books
                                orderby b.BoughtCopies descending
                                select b).Take(8).ToList();
                

            mymodel.Book = newestBooks;  
            mymodel.Account = listModel;
            
                return View(mymodel);

            }

            var booklist = (from b in books
                        where b.Title.ToLower().Contains(searchString.ToLower())
                        || b.Author.ToLower().Contains(searchString.ToLower())
                        select b).ToList();

            if(booklist.Count == 0)
            {
                return View("NoResults");
            }

            mymodel.Book = booklist;  
            mymodel.Account = listModel;
            
            return View(mymodel);
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
