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
        private BookService _bookService;
        
        public IActionResult Index(string genre)
        {
            var books = _bookService.GetAllBooks();

            return View(books);

        }

        public IActionResult Top10()
        {

            var books = _bookService.GetAllBooks();

            var top10 = (from book in books
                        orderby book.Rating descending
                        select book).ToList();

            return View(top10);
        }

        public IActionResult Details(string title){

                var books = _bookService.GetAllBooks();

                var onebook = (from newbook in books
                            where ((newbook.Title).ToLower() == title.ToLower())
                            select newbook).ToList();

                return View(onebook);
        }

        public IActionResult Genre(string genre){

            var books = _bookService.GetAllBooks();

            if(genre.Count() == 0){
                return View(books);
            }
            else{

            var genrelist = (from item in books
                            where ((item.Genre).ToLower() == genre.ToLower())
                            select item).ToList();

            return View(genrelist);
            }
        }

        public BookController()
        {
            _bookService = new BookService();
        }

    }
}