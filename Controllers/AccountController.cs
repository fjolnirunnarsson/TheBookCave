using System.Linq;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TheBookCave.Data;
using TheBookCave.Models;
using TheBookCave.Services;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService = new AccountService();
        private DataContext _db = new DataContext();
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService IAccountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = HttpContext.User.Identity.Name;
            var account = _accountService.GetLoggedInAccount(user);

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
            if(!ModelState.IsValid)
            {
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if(result.Succeeded)
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

               public IActionResult AccessDenied(){
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
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = new ApplicationUser{UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                SeedDataCreateAccount(model);

                await _userManager.AddClaimAsync(user, new Claim("FirstName", $"{model.FirstName}"));
                await _userManager.AddClaimAsync(user, new Claim("LastName", $"{model.LastName}"));
                await _userManager.AddClaimAsync(user, new Claim("Email", $"{model.Email}"));
                await _signInManager.SignInAsync(user, false);
                
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public void SeedDataCreateAccount(RegisterViewModel model)
        {
            var Accounts = new List<Account>
            {
                new Account{
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                    Email = model.Email
                }
            };
            _db.AddRange(Accounts);
            _db.SaveChanges();
        }

        [HttpGet]
        public IActionResult Edit(string email)
        {
        
            var accounts = _accountService.GetAllAccounts();

            var account = (from a in accounts
                         where a.Email == email
                         select a).First();

            return View(account);
        }
        
        [HttpPost]
        public IActionResult Edit(AccountListViewModel model)
        {
            var user = HttpContext.User.Identity.Name;
            if(!ModelState.IsValid)
            {
                return View();
            }

            _accountService.UpdateAccount(user, model);

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

