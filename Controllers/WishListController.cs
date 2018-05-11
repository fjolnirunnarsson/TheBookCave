using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TheBookCave.Data;
using TheBookCave.Services;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private WishListService _wishListService;
        private BookService _bookService;
        private DataContext _db;
        private dynamic _myModel;

        public WishListController()
        {
            _wishListService = new WishListService();
            _bookService = new BookService();
            _db = new DataContext();
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

            if (!String.IsNullOrEmpty(searchString))
            {
                var booklist = _bookService.GetSearchBooks(searchString);
                if (booklist.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = booklist;
                _myModel.Account = listModel;

                return View("../Home/Index", _myModel);
            }

            var books = _wishListService.GetWishListBooks(userId);

            _myModel.listItems = listModel;
            _myModel.bookItems = books;

            return View(_myModel);
        }

        [Authorize]
        public IActionResult AddToWishList(int bookId)
        {
            var bookAdded = _bookService.GetBookById(bookId);
            var user = WishListService.GetUser(this.HttpContext);

            _wishListService.AddToWishList(bookAdded, user);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }

        public IActionResult RemoveFromWishList(int bookId)
        {
            var bookAdded = _bookService.GetBookById(bookId);
            var user = WishListService.GetUser(this.HttpContext);

            _wishListService.RemoveFromWishList(bookAdded, user);

            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
    }
}