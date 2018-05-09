using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;
using System.Linq;
using System.Collections.Generic;
using TheBookCave.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;
using TheBookCave.Repositories;
using TheBookCave.Models.ViewModels;
using System.Dynamic;


namespace TheBookCave.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private CartRepo _cartRepo;
        private AccountService _accountService;
        private CartService _cartService;

        private BookService _bookService;
        private DataContext _db = new DataContext();

        public ShoppingCartController()
        {
            _cartService = new CartService();
            _bookService = new BookService();
            _accountService = new AccountService();
        }

        public IActionResult Index(string searchString)
        {
            var cart = CartService.GetCart(this.HttpContext);

            var cartId = cart.ShoppingCartId;

            dynamic myModel = new ExpandoObject();
            
            var cartModel = new ShoppingCartViewModel
            {
                CartItems = _cartService.GetCartItems(cartId),
                CartTotal = _cartService.GetTotal(cartId)
            };

            var accountModel = _accountService.GetAllAccounts();

            var books = (from items in _db.Books
                        join citems in _db.Carts on items.Id equals citems.BookId
                        where citems.CartId == cartId
                        select new BookListViewModel
                        {
                            Id = items.Id,
                            Image = items.Image,
                            Title = items.Title,
                            Author = items.Author,
                            AuthorId = items.AuthorId,
                            Rating = items.Rating,
                            Price = items.DiscountPrice,
                            Genre = items.Genre,
                            BoughtCopies = items.BoughtCopies,
                            Year = items.Year,
                            Description = items.Description,
                            Quantity = citems.Quantity,
                        }).ToList();

            myModel.cartItems = cartModel;
            myModel.bookItems = books;
            myModel.account = accountModel;
            

            return View(myModel);
        }

        [Authorize]
        public IActionResult AddToCart(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var cart = CartService.GetCart(this.HttpContext);


            _cartService.AddToCart(bookAdded, this.HttpContext);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var cart = CartService.GetCart(this.HttpContext);

            _cartService.RemoveFromCart(bookAdded, this.HttpContext);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout(string email)
        {
                    
            var accounts = _accountService.GetAllAccounts();

            var account = (from a in accounts
                         where a.Email == email
                         select a).SingleOrDefault();
            return View(account);
        }

        [HttpPost]
        public IActionResult Checkout(AccountListViewModel updatedAccount)
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
            return View();
        }

        public IActionResult ConfirmationStep()
        {
            return View();
        }
    }
}
