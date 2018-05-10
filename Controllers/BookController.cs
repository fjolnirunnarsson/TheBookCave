using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;
using System.Dynamic;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        
        public BookController()
        {
            _bookService = new BookService();
        }
        public IActionResult Index(string searchString)
        {
            var books = _bookService.GetAllBooks();

            if(String.IsNullOrEmpty(searchString))
            {
                return View(books);
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

        public IActionResult Top10(string searchString)
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
            
            var top10 = (from book in books
                        orderby book.Rating descending
                        select book).Take(10).ToList();

            if(top10.Count == 0)
            {
                return View("NotFound");
            }

            return View(top10);
        }

        [HttpGet]
        public IActionResult Details(string title, string searchString)
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

            var onebook = (from newbook in books
                        where newbook.Title.ToLower() == title.ToLower()
                        select newbook).First();

            var reviews = _bookService.GetAllReviews();

            var thisbookreviews = (from rev in reviews
                                where (rev.BookId == onebook.Id)
                                select rev).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Book = onebook;
            mymodel.Reviews = thisbookreviews;

            return View(mymodel);
        }
        [HttpPost]
        public IActionResult Details(ReviewInputModel review){

            var user = HttpContext.User.Identity.Name;

            if(!ModelState.IsValid)
            {
                return View();
            }

            SeedDataCreate(review, user);

            using (var db = new DataContext())
            {
                var reviews = _bookService.GetAllReviews();

                var onebook = (from newbook in db.Books
                            where newbook.Id == review.BookId
                            select newbook).First();
                            
                var allreviews = (from newreview in db.Reviews
                            where newreview.BookId == onebook.Id
                            select newreview).ToList();

                onebook.Rating = Math.Round(GetRating(allreviews),2);
                db.SaveChanges();
            }

            var books = _bookService.GetAllBooks();

            var book = (from b in books
                        where b.Id == review.BookId
                        select b).First();

            return RedirectToAction("Details", book);
        }

        public double GetRating(List<Review> reviews)
        {
            double rating = 0;
            foreach(var review in reviews){
                rating += review.Rating;
            }
            rating = rating/reviews.Count();
            return rating;
        }
        
        [HttpGet]
        public static void SeedDataCreate(ReviewInputModel review, string user)
        {
            var db = new DataContext();
            
            var Reviews = new List<Review>{

                new Review{
                    Rating = review.Rating,
                    Comment = review.Comment,
                    UserName = user,
                    BookId = review.BookId
                }
            };
            db.AddRange(Reviews);
            db.SaveChanges();
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