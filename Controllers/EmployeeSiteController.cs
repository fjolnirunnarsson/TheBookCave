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
    [Authorize(Roles = "Admin")]
    public class EmployeeSiteController : Controller
    {
         private BookService _bookService;
        private AccountService _accountService;
        
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
    
        public EmployeeSiteController(SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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

            var tempaccount = (from a in accounts
                        where a.Email == model.Email
                        select a).SingleOrDefault();
                        
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "You have to fill in every field!";
                return View(); 
            }
            if(tempaccount != null &&  model.Email == tempaccount.Email){
                ViewData["ErrorMessage"] = "This user already has an account!";
                return View(); 
            }
            
            
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            //var adminRole = await _roleManager.FindByNameAsync("Admin");
    
            if(result.Succeeded)
            {
                SeedDataCreateAccount(model);
                // The User is successfully registered
                // Add the concatenated first and last name as fullname in claims
                await _userManager.AddClaimAsync(user, new Claim("FirstName", $"{model.FirstName}"));
                await _userManager.AddClaimAsync(user, new Claim("LastName", $"{model.LastName}"));
                await _userManager.AddClaimAsync(user, new Claim("Email", $"{model.Email}"));
                //await _signInManager.SignInAsync(user, false);

                ApplicationUser newAdmin = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRoleAsync(newAdmin, "Admin");
            }
            return View("Created");
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
                        orderby book.Title ascending
                        select book).ToList();

            return View(booklist);
        }

                public IActionResult OrderbyAuthor()
        {
            var books = _bookService.GetAllBooks();
            var booklist = (from b in books
                orderby b.Author ascending
                select b).ToList();

            return View("Index", booklist);
        }
                public IActionResult OrderbyGenre()
        {
            var books = _bookService.GetAllBooks();
            var booklist = (from b in books
                orderby b.Genre ascending
                select b).ToList();

            return View("Index", booklist);
        }
                public IActionResult OrderbyPrice()
        {
            var books = _bookService.GetAllBooks();
            var booklist = (from b in books
                orderby b.Price ascending
                select b).ToList();

            return View("Index", booklist);
        }
                public IActionResult OrderbyDiscount()
        {
            var books = _bookService.GetAllBooks();
            var booklist = (from b in books
                orderby b.Discount ascending
                select b).ToList();

            return View("Index", booklist);
        }

        public IActionResult OrderbyQuantity()
        {
            var books = _bookService.GetAllBooks();
            var booklist = (from b in books
                orderby b.Quantity ascending
                select b).ToList();

            return View("Index", booklist);
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
        public IActionResult Change(BookInputModel updatedBook)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            _bookService.ProcessBook(updatedBook);

            using (var db = new DataContext())
            {
                var onebook = (from b in db.Books
                            where b.Id == updatedBook.Id
                            select b).FirstOrDefault();

                onebook.Title = updatedBook.Title;
                onebook.Image = updatedBook.Image;
                onebook.Author = updatedBook.Author;
                onebook.Genre = updatedBook.Genre;
                onebook.Quantity = updatedBook.Quantity;
                onebook.Price = updatedBook.Price;
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

        public void SeedDataCreate(BookInputModel book)
        {
            _bookService.ProcessBook(book);

            var db = new DataContext();
                var Books = new List<Book>
                {
                    new Book{
                        Title = book.Title, 
                        Author = book.Author, 
                        Description = book.Description,
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