using System;
using System.Linq;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;
        private WishListService _wishListService;
        private dynamic _myModel;

        public HomeController()
        {
            _bookService = new BookService();
            _wishListService = new WishListService();
            _myModel = new ExpandoObject();
        }

        public IActionResult Index(string searchString)
        {

            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            if (String.IsNullOrEmpty(searchString))
            {
                var newestBooks = _bookService.GetBooksBoughtOrder();

                _myModel.Book = newestBooks;
                _myModel.Account = listModel;

                return View(_myModel);
            }

            var booklist = _bookService.GetSearchBooks(searchString);
            if (booklist.Count == 0)
            {
                return View("NoResults");
            }

            _myModel.Book = booklist;
            _myModel.Account = listModel;

            return View(_myModel);
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
            if (!String.IsNullOrEmpty(searchString))
            {
                var booklist = _bookService.GetSearchBooks(searchString);
                if (booklist.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = booklist;
                _myModel.Account = listModel;

                return View("Index", _myModel);
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
            if (!String.IsNullOrEmpty(searchString))
            {
                var booklist = _bookService.GetSearchBooks(searchString);
                if (booklist.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = booklist;
                _myModel.Account = listModel;

                return View("Index", _myModel);
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
            if (!String.IsNullOrEmpty(searchString))
            {
                var booklist = _bookService.GetSearchBooks(searchString);
                if (booklist.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = booklist;
                _myModel.Account = listModel;

                return View("Index", _myModel);
            }

            return View();
        }
    }
}
