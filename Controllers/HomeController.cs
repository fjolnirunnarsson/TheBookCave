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
                
                var newestBooks = (from b in books
                                orderby b.BoughtCopies descending
                                select b).Take(8).ToList();
                
                return View(newestBooks);
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
    }
}
