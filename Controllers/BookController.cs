using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;
using System.Dynamic;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private WishListService _wishListService;
        private dynamic myModel = new ExpandoObject();
        
        public BookController()
        {
            _bookService = new BookService();
            _wishListService = new WishListService();
        }
        public IActionResult Index(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(String.IsNullOrEmpty(searchString))
            {
                return View(books);
            }

            var bookList = _bookService.GetSearchBooks(searchString);


            if(bookList.Count == 0)
            {
                return View("NoResults");
            }

            return View(bookList);
        }

        public IActionResult Top10(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;

            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId)
            };

            var books = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                
                if(bookList.Count == 0)
                {
                    return View("NoResults");
                }

                myModel.Book = bookList;
                myModel.Account = listModel;
                return View("Index", myModel);
            }
            
            var top10 = _bookService.GetTop10();

            if(top10.Count == 0)
            {
                return View("NotFound");
            }

            myModel.Book = top10;
            myModel.Account = listModel;
            
            return View(myModel);
        }

       [HttpGet]
        public IActionResult Details(string title, string searchString)
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
                var bookList = _bookService.GetSearchBooks(searchString);
                
                if(bookList.Count == 0)
                {
                    return View("NoResults");
                }
                return View("Index", bookList);
            }

            myModel.Book = _bookService.GetBookByTitle(title);
            myModel.Reviews = _bookService.GetBookReviews(title);
            myModel.Account = listModel;

            return View(myModel);
        }
        
        [HttpPost]
        public IActionResult Details(ReviewInputModel review){

            var user = HttpContext.User.Identity.Name;

            if(!ModelState.IsValid)
            {
                return View();
            }

            _bookService.SeedDataCreate(review, user);

            _bookService.UpdateBookRating(review);

            var books = _bookService.GetAllBooks();

            var book = _bookService.GetBookByReview(review);

            return RedirectToAction("Details", book);
        }
        public IActionResult Genre(string genre, string searchString)
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

            if(String.IsNullOrEmpty(genre))
            {
                return View(books);
            }

            else
            {
                var genrelist = (from item in books
                                where item.Genre.ToLower() == genre.ToLower()
                                select item).ToList();

                if(genrelist.Count == 0)
                {
                    return View("NotFound");
                }

                return View(genrelist);
            }
        }

        public IActionResult OrderAlphabetical(string searchString)
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

            var orderedBooks = (from b in books
                            orderby b.Title ascending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult PriceLowToHigh(string searchString)
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

            var orderedBooks = (from b in books
                            orderby b.Price ascending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult PriceHighToLow(string searchString)
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

            var orderedBooks = (from b in books
                            orderby b.Price descending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult Newest(string searchString)
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

            var orderedBooks = (from b in books
                            orderby b.Id descending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult Sale(string searchString)
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

            var saleBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            select b).ToList();

            return View("Index", saleBooks);
        }

        public IActionResult SaleOrderAlphabetical(string searchString)
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

            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Title ascending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult SalePriceLowToHigh(string searchString)
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

            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Price ascending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult SalePriceHighToLow(string searchString)
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

            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Price descending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

        public IActionResult SaleNewest(string searchString)
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

            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Id descending
                            select b).ToList();

            return View("Index", orderedBooks);
        }

    }
}