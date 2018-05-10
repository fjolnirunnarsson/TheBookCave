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
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

        public IActionResult Index()
        {

            var user = HttpContext.User.Identity.Name;

            var accounts = _accountService.GetAllAccounts();

            var account = (from a in accounts
                        where a.Email == user
                        select a).First();

            return View(account);
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
        public IActionResult Edit(AccountListViewModel updatedAccount)
        {
            
            if(!ModelState.IsValid)
            {
                return View();
            }

            using (var db = new DataContext())
            {
                var account = (from a in db.Accounts
                            where a.Email == updatedAccount.Email
                            select a).First();

                account.FirstName = updatedAccount.FirstName;
                account.LastName = updatedAccount.LastName;
                account.Email = updatedAccount.Email;
                account.ProfilePicture = updatedAccount.ProfilePicture;
                account.FavoriteBook = updatedAccount.FavoriteBook;
                account.BillingAddressStreet = updatedAccount.BillingAddressStreet;
                account.BillingAddressHouseNumber = updatedAccount.BillingAddressHouseNumber;
                account.BillingAddressLine2 = updatedAccount.BillingAddressLine2;
                account.BillingAddressCity = updatedAccount.BillingAddressCity;
                account.BillingAddressCountry = updatedAccount.BillingAddressCountry;
                account.BillingAddressZipCode = updatedAccount.BillingAddressZipCode;
                account.DeliveryAddressStreet = updatedAccount.DeliveryAddressStreet;
                account.DeliveryAddressHouseNumber = updatedAccount.DeliveryAddressHouseNumber;
                account.DeliveryAddressLine2 = updatedAccount.DeliveryAddressLine2;
                account.DeliveryAddressCity = updatedAccount.DeliveryAddressCity;
                account.DeliveryAddressCountry = updatedAccount.DeliveryAddressCountry;
                account.DeliveryAddressZipCode = updatedAccount.DeliveryAddressZipCode;
                //account.SameAdresses = updatedAccount.SameAdresses;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Purchases() 
        {
            var books = _accountService.GetAllPurchases(this.HttpContext);

            dynamic myModel = new ExpandoObject();

            myModel.books = books;

            return View(books);
        }
    }
}

