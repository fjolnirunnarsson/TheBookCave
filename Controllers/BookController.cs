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
        
        public IActionResult Index()
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
                            where (newbook.Title == title)
                            select newbook).ToList();

                return View(onebook);
        }

        public BookController()
        {
            _bookService = new BookService();
        }

    }
}