using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _BookService;
        
        public BookController()
        {
            _BookService = new BookService();
        }
        public IActionResult Index(string searchString)
        {
            var books = _BookService.GetAllBooks();

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
                return View("NotFound");
            }

            return View(booklist);
        }

        public IActionResult Top10()
        {

            var books = _BookService.GetAllBooks();

            var top10 = (from book in books
                        orderby book.Rating descending
                        select book).Take(10).ToList();

            return View(top10);
        }

        public IActionResult BestSellers()
        {
            var books = _BookService.GetAllBooks();
            
            var bestsellers = (from book in books
                        orderby book.BoughtCopies descending
                        select book).ToList();

            return View(bestsellers);
        }

        public IActionResult Details(string title){

                var books = _BookService.GetAllBooks();

                var onebook = (from newbook in books
                            where ((newbook.Title).ToLower() == title.ToLower())
                            select newbook).ToList();

                return View(onebook);
        }
        public IActionResult Genre(string genre){

            var books = _BookService.GetAllBooks();

            if(genre.Count() == 0){
                return View(books);
            }
            else{

            var genrelist = (from item in books
                            where ((item.Genre).ToLower() == genre.ToLower())
                            select item).ToList();

            ViewBag.Heading = genre;

            return View(genrelist);
            }
        }

        public IActionResult OrderAlphabetical()
        {
            var books = _BookService.GetAllBooks();

            var booklist = (from b in books
                            orderby b.Title ascending
                            select b).ToList();

            return View(booklist);
        }

        public IActionResult PriceLowToHigh()
        {
            var books = _BookService.GetAllBooks();

            var booklist = (from b in books
                            orderby b.Price ascending
                            select b).ToList();

            return View(booklist);
        }

        public IActionResult PriceHighToLow()
        {
            var books = _BookService.GetAllBooks();

            var booklist = (from b in books
                            orderby b.Price descending
                            select b).ToList();

            return View(booklist);
        }

        public IActionResult Newest()
        {
            var books = _BookService.GetAllBooks();

            var booklist = (from b in books
                            orderby b.Id descending
                            select b).ToList();

            return View(booklist);
        }

    }
}