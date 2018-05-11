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
        private dynamic myModel = new ExpandoObject();
        
        public HomeController()
        {
            _bookService = new BookService();
            _wishListService = new WishListService();
        }
        public IActionResult Index(string searchString)
        {

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
                

            myModel.Book = newestBooks;  
            myModel.Account = listModel;
            
                return View(myModel);

            }

            var booklist = (from b in books
                        where b.Title.ToLower().Contains(searchString.ToLower())
                        || b.Author.ToLower().Contains(searchString.ToLower())
                        select b).ToList();

            if(booklist.Count == 0)
            {
                return View("NoResults");
            }

            myModel.Book = booklist;  
            myModel.Account = listModel;
            
            return View(myModel);
        }
        public IActionResult AboutUs(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

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

                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            return View();
        }

        public IActionResult TermsAndConditions(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

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
                    myModel.Book = booklist;
                    myModel.Account = listModel;
                return View("Index", myModel);
            }

            return View();
        }

        public IActionResult Help(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

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
                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            return View();
        }
    }
}
