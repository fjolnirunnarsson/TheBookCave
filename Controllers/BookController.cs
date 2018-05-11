using System;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private WishListViewModel _wishModel;
        private WishListService _wishListService;
        private dynamic _myModel;

        public BookController()
        {
            _bookService = new BookService();
            _wishModel = new WishListViewModel();
            _wishListService = new WishListService();
            _myModel = new ExpandoObject();
        }

        public IActionResult Index(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;

            var _wishModel = _wishListService.GetWishListItems(userId);
            
            var books = _bookService.GetAllBooks();
            if (String.IsNullOrEmpty(searchString))
            {
                _myModel.Book = books;
                _myModel.Account = _wishModel;

                return View(_myModel);
            }

            var bookList = _bookService.GetSearchBooks(searchString);
            if (bookList.Count == 0)
            {
                return View("NoResults");
            }

            _myModel.Book = bookList;
            _myModel.Account = _wishModel;

            return View(_myModel);
        }

        [HttpGet]
        public IActionResult Details(string title, string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            _myModel.Book = _bookService.GetBookByTitle(title);
            _myModel.Reviews = _bookService.GetBookReviews(title);
            _myModel.Account = _wishModel;

            return View(_myModel);

        }

        [HttpPost]
        public IActionResult Details(ReviewInputModel review)
        {
            var user1 = WishListService.GetUser(this.HttpContext);
            var userId = user1.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var user = HttpContext.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View();
            }

            var book = _bookService.GetBookByReview(review);

            _bookService.SeedDataCreateReview(review, user);
            _bookService.UpdateBookRating(review);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
        }

        public IActionResult Top10(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var top10 = _bookService.GetTop10();
            if (top10.Count == 0)
            {
                return View("NotFound");
            }

            _myModel.Book = top10;
            _myModel.Account = _wishModel;

            return View(_myModel);
        }

        public IActionResult Newest(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksByNewest();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Index", _myModel);
        }

        public IActionResult Sale(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var saleBooks = _bookService.GetBooksOnSale();

            _myModel.Book = saleBooks;
            _myModel.Account = _wishModel;

            return View(_myModel);
        }

        public IActionResult Genre(string genre, string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var books = _bookService.GetAllBooks();
            if (String.IsNullOrEmpty(genre))
            {
                _myModel.Book = books;
                _myModel.Account = _wishModel;

                return View(_myModel);
            }

            else
            {
                var genrelist = _bookService.GetBooksByGenre(genre);

                if (genrelist.Count == 0)
                {
                    return View("NotFound");
                }

                _myModel.Book = genrelist;
                _myModel.Account = _wishModel;

                return View(_myModel);
            }
        }

        public IActionResult OrderAlphabetical(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            
            var _wishModel = _wishListService.GetWishListItems(userId);

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksTitleOrder();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Index", _myModel);
        }

        public IActionResult PriceLowToHigh(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _wishModel);
            }

            var orderedBooks = _bookService.GetBooksPriceOrderLH();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Index", _myModel);
        }

        public IActionResult PriceHighToLow(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksPriceOrderHL();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Index", _myModel);
        }

        public IActionResult SaleOrderAlphabetical(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleAZ();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Sale", _myModel);
        }

        public IActionResult SalePriceLowToHigh(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId); 

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _wishModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleLH();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Sale", _myModel);
        }

        public IActionResult SalePriceHighToLow(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }
                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleHL();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Sale", _myModel);
        }

        public IActionResult SaleNewest(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var _wishModel = _wishListService.GetWishListItems(userId);

            var books = _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = _wishModel;

                return View("Index", _myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleNewest();

            _myModel.Book = orderedBooks;
            _myModel.Account = _wishModel;

            return View("Sale", _myModel);
        }
    }
}