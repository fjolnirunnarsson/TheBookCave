using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using TheBookCave.Models.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private WishListService _wishListService;

        private BookService _bookService;
        private DataContext _db = new DataContext();

        public WishListController()
        {
            _wishListService = new WishListService();
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;

            dynamic myModel = new ExpandoObject();
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            var books = (from items in _db.Books
                        join citems in _db.Lists on items.Id equals citems.BookId
                        where citems.UserId == userId
                        select new BookListViewModel
                        {
                            Id = items.Id,
                            Image = items.Image,
                            Title = items.Title,
                            Author = items.Author,
                            AuthorId = items.AuthorId,
                            Rating = items.Rating,
                            Price = items.DiscountPrice,
                            Genre = items.Genre,
                            BoughtCopies = items.BoughtCopies,
                            Year = items.Year,
                            Description = items.Description,
                        }).ToList();

            myModel.listItems = listModel;
            myModel.bookItems = books;

            return View(myModel);
        }

        [Authorize]
        public IActionResult AddToWishList(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var user = WishListService.GetUser(this.HttpContext);


            _wishListService.AddToWishList(bookAdded, this.HttpContext);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromWishList(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var user = WishListService.GetUser(this.HttpContext);

            _wishListService.RemoveFromWishList(bookAdded, this.HttpContext);

            return RedirectToAction("Index");
        }
    }
}