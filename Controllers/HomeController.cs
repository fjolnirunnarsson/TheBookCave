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
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
  private BookService _bookService;
        
        public HomeController()
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
                return View("NotFound");
            }

            return View(booklist);
        }

        public IActionResult Top10()
        {
            var books = _bookService.GetAllBooks();

            return View(books);
        }

        public IActionResult Details(){

            var books = _bookService.GetAllBooks();
            
            return View(books);
        }

        public IActionResult OrderAlphabetical()
        {
            var books = _bookService.GetAllBooks();

            var booklist = (from b in books
                            orderby b ascending
                            select b).ToList();

            return View(booklist);
        }
    }
}
