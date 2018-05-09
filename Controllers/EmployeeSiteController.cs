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

namespace TheBookCave.Controllers
{
    //[Authorize(Roles = "Loser")]
    public class EmployeeSiteController : Controller
    {
         private BookService _bookService;
        private AccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
    
        public EmployeeSiteController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = new AccountService();
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(); //Sagði í fyrirlestri að hér ætti að hafa client side validation
            }
            
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user,"Admin");


            if(result.Succeeded)
            {
                SeedDataCreateAccount(model);
                // The User is successfully registered
                // Add the concatenated first and last name as fullname in claims
                await _userManager.AddClaimAsync(user, new Claim("FirstName", $"{model.FirstName}"));
                await _userManager.AddClaimAsync(user, new Claim("LastName", $"{model.LastName}"));
                await _userManager.AddClaimAsync(user, new Claim("Email", $"{model.Email}"));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public static void SeedDataCreateAccount(RegisterViewModel model)
        {
             var db = new DataContext();
                var Accounts = new List<Account>
                {
                    new Account{
                        FirstName = model.FirstName, 
                        LastName = model.LastName,
                        Email = model.Email
                    }
                };
                db.AddRange(Accounts);
                db.SaveChanges();
        }

        
        
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();

            var booklist = (from book in books
                        select book).ToList();

            return View(booklist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel book)
        {
            SeedDataCreate(book);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Change(int id)
        {

            var books = _bookService.GetAllBooks();

            var onebook = (from b in books
                         where b.Id == id
                         select b).SingleOrDefault();

            return View(onebook);
        }

        [HttpPost]
        public IActionResult Change(BookListViewModel updatedBook)
        {
            using (var db = new DataContext())
            {
                var onebook = (from b in db.Books
                            where b.Id == updatedBook.Id
                            select b).FirstOrDefault();

                onebook.Title = updatedBook.Title;
                onebook.Image = updatedBook.Image;
                onebook.Author = updatedBook.Author;
                onebook.Quantity = updatedBook.Quantity;
                onebook.Price = updatedBook.Price;
                onebook.Year = updatedBook.Year;
                onebook.Description = updatedBook.Description;
                onebook.Discount = updatedBook.Discount;
                onebook.DiscountPrice = System.Math.Round((1 - updatedBook.Discount/100) * updatedBook.Price,2);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            // if(id == null) { return View("NotFound") }

            var books = _bookService.GetAllBooks();

            var book = (from b in books
                        where b.Id == id
                        select b).SingleOrDefault();

            // if(student == null) { return View("NotFound"); }

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

        public static void SeedDataCreate(BookInputModel book)
        {
            var db = new DataContext();
                var Books = new List<Book>
                {
                    new Book{
                        Title = book.Title, 
                        Author = book.Author, 
                        Description = book.Description,
                        Year = book.Year, 
                        Image = book.Image, 
                        Genre = book.Genre,  
                        Price = book.Price, 
                        Discount = book.Discount, 
                        Quantity = book.Quantity, 
                    }
                };
                db.AddRange(Books);
                db.SaveChanges();    
        }

    }
}