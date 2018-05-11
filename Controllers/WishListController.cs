using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using TheBookCave.Models.ViewModels;
using TheBookCave.Data;
using TheBookCave.Services;
using System;

namespace TheBookCave.Controllers
{
    [Authorize]
    public class WishListController : Controller
    {
        private WishListService _wishListService;
        private BookService _bookService;
        private DataContext _db = new DataContext();
        private dynamic myModel = new ExpandoObject();
        public WishListController()
        {
            _wishListService = new WishListService();
            _bookService = new BookService();
        }
        public IActionResult Index(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            var allBooks = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var booklist = (from b in allBooks
                                where b.Title.ToLower().Contains(searchString.ToLower())
                                || b.Author.ToLower().Contains(searchString.ToLower())
                                select b).ToList();
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                        myModel.Book = booklist;
                        myModel.Account = listModel;

                return View("../Home/Index", myModel);
            }

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

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
        }

        public IActionResult RemoveFromWishList(int bookId, bool breyta)
        {
            if(breyta == false)
            {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var user = WishListService.GetUser(this.HttpContext);

            _wishListService.RemoveFromWishList(bookAdded, this.HttpContext);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
            }
            else
            {
                var books = _bookService.GetAllBooks();

                var bookAdded = (from book in books
                                where book.Id == bookId
                                select book).SingleOrDefault();

                var user = WishListService.GetUser(this.HttpContext);

                _wishListService.RemoveFromWishList(bookAdded, this.HttpContext);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
            }
        }
    }
}