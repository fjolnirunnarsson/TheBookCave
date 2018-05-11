using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TheBookCave.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace TheBookCave.Controllers
{
    public class EmployeeSiteController : Controller
    {
        private BookService _bookService;
        private AccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
    
        public EmployeeSiteController(SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _bookService = new BookService();
            _accountService = new AccountService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var accounts = _accountService.GetAllAccounts();

            var tempaccount = _accountService.GetTempAccount(model);
                        
            if(!ModelState.IsValid)
            {
                return View(); 
            }
            if(tempaccount != null &&  model.Email == tempaccount.Email){
                ViewData["ErrorMessage"] = "This user already has an account!";
                return View(); 
            }
    
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);
    
            if(result.Succeeded)
            {
                _bookService.SeedDataCreateAccount(model);

                await _userManager.AddClaimAsync(user, new Claim("FirstName", $"{model.FirstName}"));
                await _userManager.AddClaimAsync(user, new Claim("LastName", $"{model.LastName}"));
                await _userManager.AddClaimAsync(user, new Claim("Email", $"{model.Email}"));

                ApplicationUser newAdmin = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRoleAsync(newAdmin, "Admin");
            }
            return View("Created");
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var bookList = _bookService.GetBooksTitleOrder();
                        
            return View(bookList);
        }

        public IActionResult OrderbyAuthor()
        {
            var books = _bookService.GetAllBooks();
            var bookList = _bookService.GetBooksByAuthorAZ();

            return View("Index", bookList);
        }

        public IActionResult OrderbyGenre()
        {
            var books = _bookService.GetAllBooks();
            var booklist = _bookService.GetBooksGenreOrderAZ();

            return View("Index", booklist);
        }

        public IActionResult OrderbyPrice()
        {
            var booklist = _bookService.GetBooksPriceOrderLH();
            return View("Index", booklist);
        }

        public IActionResult OrderbyDiscount()
        {
            var booklist = _bookService.GetBooksByDiscount();
            return View("Index", booklist);
        }

        public IActionResult OrderbyQuantity()
        {
            var booklist = _bookService.GetBooksByQuantity();
            return View("Index", booklist);
        }

        public IActionResult OrderbySold()
        {
            var booklist = _bookService.GetBooksOrderSold();
            return View("Index", booklist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel book)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            _bookService.ProcessBook(book);
            _bookService.SeedDataCreateBook(book);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Change(int id)
        {
            var book = _bookService.GetBookById(id); 
            return View(book);
        }

        [HttpPost]
        public IActionResult Change(BookInputModel updatedBook)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            _bookService.ProcessBook(updatedBook);
            _bookService.SeedDataChangeBook(updatedBook);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if(id == null)
            {
                return View("NotFound");
            }

                var books = _bookService.GetAllBooks();

                var book = (from b in books
                            where b.Id == id
                            select b).First();

            if(book == null)
            {
                return View("NotFound");
            }

            return View(book);
        }
        
        [HttpPost]
        public IActionResult Delete(BookInputModel deleteBook)
        {
            using (var db = new DataContext())
            {
                var onebook = (from b in db.Books
                    where b.Id == deleteBook.Id
                    select b).FirstOrDefault();
                db.Books.Remove(onebook);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}