using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
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

        public IActionResult Index(string email){

            var accounts = _accountService.GetAllAccounts();

            var account = (from a in accounts
                        where a.Email == email
                        select a).SingleOrDefault();

            return View(account);
        }

        [HttpGet]
        public IActionResult Edit(string email)
        {
        
            var accounts = _accountService.GetAllAccounts();

            var account = (from a in accounts
                         where a.Email == email
                         select a).SingleOrDefault();

            return View(account);
        }
        
        [HttpPost]
        public IActionResult Edit(AccountListViewModel updatedAccount)
        {
            using (var db = new DataContext())
            {
                var account = (from a in db.Accounts
                            where a.Email == updatedAccount.Email
                            select a).FirstOrDefault();

                account.FirstName = updatedAccount.FirstName;
                account.LastName = updatedAccount.LastName;
                account.Email = updatedAccount.Email;
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

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            ApplicationUser user = _userManager.FindByNameAsync(model.UserName).Result;

            var result = _userManager.ResetPasswordAsync(user, model.Token, model.Password).Result;



            if(result.Succeeded)
            {
                ViewBag.Message = "Password reset successful!";
                return View("Success");
            }
            else
            {
                ViewBag.Message = "Error while resetting the password!";
                return View("Error");
            }
        }
    }
}