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
            var user = WishListService.GetUser(this.HttpContext);

            var userId = user.UserId;

            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId)
            };

            var books = _bookService.GetAllBooks();

            if(String.IsNullOrEmpty(searchString))
            {
                myModel.Book = books;
                myModel.Account = listModel;

                return View(myModel);
            }

            var bookList = _bookService.GetSearchBooks(searchString);


            if(bookList.Count == 0)
            {
                return View("NoResults");
            }

            myModel.Book = bookList;
            myModel.Account = listModel;
            return View(myModel);
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

                myModel.Book = bookList;

                return View("Index", myModel);
            }

            myModel.Book = _bookService.GetBookByTitle(title);
            myModel.Reviews = _bookService.GetBookReviews(title);
            myModel.Account = listModel;

            return View(myModel);
        }
        
        [HttpPost]
        public IActionResult Details(ReviewInputModel review)
        {

            var user1 = WishListService.GetUser(this.HttpContext);

            var userId = user1.UserId;
            
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            var user = HttpContext.User.Identity.Name;

            if(!ModelState.IsValid)
            {
                return View();
            }

            _bookService.SeedDataCreate(review, user);

            _bookService.UpdateBookRating(review);

            var books = _bookService.GetAllBooks();

            var book = _bookService.GetBookByReview(review);

            myModel.Book = book;
            myModel.Account = listModel;
            return RedirectToAction("Details", myModel);
        }
        public IActionResult Genre(string genre, string searchString)
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

                myModel.Book = bookList;
                myModel.Account = listModel;

                return View("Index", myModel);
            }

            if(String.IsNullOrEmpty(genre))
            {
                myModel.Book = books;
                myModel.Account = listModel;

                return View(myModel);
            }

            else
            {
                var genrelist = _bookService.GetBooksByGenre(genre);

                if(genrelist.Count == 0)
                {
                    return View("NotFound");
                }

                myModel.Book = genrelist;
                myModel.Account = listModel;

                return View(myModel);
            }
        }

        public IActionResult OrderAlphabetical(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }

                myModel.Book = booklist;
                myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksTitleOrder();

            myModel.Book = orderedBooks;
            myModel.Account = listModel;

            return View("Index", myModel);
        }

        public IActionResult PriceLowToHigh(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }

                myModel.Book = booklist;
                myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksPriceOrderLH();

                myModel.Book = orderedBooks;
                myModel.Account = listModel;

            return View("Index", myModel);
        }

        public IActionResult PriceHighToLow(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }

                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksPriceOrderHL();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Index", myModel);
        }

        public IActionResult Newest(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }

                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksByNewest();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Index", myModel);
        }

        public IActionResult Sale(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var saleBooks = _bookService.GetBooksOnSale();

                    myModel.Book = saleBooks;
                    myModel.Account = listModel;

            return View(myModel);
        }

        public IActionResult SaleOrderAlphabetical(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleAZ();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Sale", myModel);
        }

        public IActionResult SalePriceLowToHigh(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }

                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleLH();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Sale", myModel);
        }

        public IActionResult SalePriceHighToLow(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleHL();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Sale", myModel);
        }

        public IActionResult SaleNewest(string searchString)
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
                var booklist = _bookService.GetSearchBooks(searchString);
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                    myModel.Book = booklist;
                    myModel.Account = listModel;

                return View("Index", myModel);
            }

            var orderedBooks = _bookService.GetBooksOnSaleNewest();

                    myModel.Book = orderedBooks;
                    myModel.Account = listModel;

            return View("Sale", myModel);
        }

    }
}