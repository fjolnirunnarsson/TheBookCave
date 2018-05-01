using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
        private AuthorService _authorService;
        public IActionResult Index()
        {
            var authors = _authorService.GetAllAuthors();
            
            return View(authors);
        }
        public HomeController()
        {
            _authorService = new AuthorService();
        }
    }
}
