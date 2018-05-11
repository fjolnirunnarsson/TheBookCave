using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TheBookCave.Data;
using TheBookCave.Models;
using TheBookCave.Services;
using TheBookCave.Models.ViewModels;
using TheBookCave.Models.InputModels;
using System;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService;
        private BookService _bookService;
        private dynamic _myModel;
        private DataContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService IAccountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = new AccountService();
            _bookService = new BookService();
            _myModel = new ExpandoObject();
            _db = new DataContext();
        }

        public IActionResult Index(string searchString)
        {
            var user = HttpContext.User.Identity.Name;
            var account = _accountService.GetLoggedInAccount(user);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;

                return View("../Home/Index", _myModel);
            }

            return View(account);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _accountService.SeedDataCreateAccount(model);

                await _userManager.AddClaimAsync(user, new Claim("FirstName", $"{model.FirstName}"));
                await _userManager.AddClaimAsync(user, new Claim("LastName", $"{model.LastName}"));
                await _userManager.AddClaimAsync(user, new Claim("Email", $"{model.Email}"));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(string email)
        {
            var account = _accountService.GetEditAccount(email);
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(AccountListViewModel model)
        {
            var user = HttpContext.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View();
            }

            _accountService.UpdateAccountEdit(user, model);

            return RedirectToAction("Index");
        }
        public IActionResult Purchases()
        {
            var user = HttpContext.User.Identity.Name;
            var books = _accountService.GetAllPurchases(user);

            dynamic myModel = new ExpandoObject();
            myModel.books = books;

            return View(books);
        }
    }
}

