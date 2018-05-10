using Microsoft.AspNetCore.Mvc;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using TheBookCave.Models.ViewModels;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace TheBookCave.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
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
            var allBooks = _bookService.GetAllBooks();

            if(!String.IsNullOrEmpty(searchString))
            {
                var booklist = (from b in allBooks
                                where b.Title.ToLower().Contains(searchString.ToLower())
                                || b.Author.ToLower().Contains(searchString.ToLower())
                                select b).ToList();
                
                if(booklist.Count == 0)
                {
                    return View("NoResults");
                }
                return View("../Home/Index", booklist);
            }
            
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
        public IActionResult Checkout(AccountInputModel updatedAccount)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            _accountService.ProcessAccount(updatedAccount);
            
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
            return RedirectToAction("ReviewStep");
        }

        [HttpGet]
        public IActionResult ReviewStep()
        {
            var user = HttpContext.User.Identity.Name;

            var cart = CartService.GetCart(this.HttpContext);

            var cartId = cart.ShoppingCartId;

            dynamic myModel = new ExpandoObject();
            
            var cartModel = new ShoppingCartViewModel
            {
                CartItems = _cartService.GetCartItems(cartId),
                CartTotal = _cartService.GetTotal(cartId)
            };

            var accounts = _accountService.GetAllAccounts();

            var accountmodel = (from a in accounts
                         where a.Email == user
                         select a).SingleOrDefault();

            myModel.cartItems = cartModel;
            myModel.account = accountmodel;

            return View(myModel);
        }

        public IActionResult ConfirmationStep()
        {
            var user = HttpContext.User.Identity.Name;
            var cart = CartService.GetCart(this.HttpContext);
            var cartId = cart.ShoppingCartId;

            dynamic myModel = new ExpandoObject();
            
            var cartItems = _cartService.GetCartItems(cartId);
            
            foreach (var item in cartItems)
            {
                var cartItem = new Purchased()
                {
                    CartId = item.CartId,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    DateCreated = item.DateCreated,
                    Book = item.Book
                };
                _db.Purchased.Add(cartItem);
            }

            //Change quantity of book and bought copies
            foreach (var item in cartItems)
            {
                    var thebook = (from b in _db.Books
                            where b.Id == item.BookId
                            select b).SingleOrDefault();

                    thebook.Quantity -= item.Quantity;
                    thebook.BoughtCopies += item.Quantity;
            }
            
            foreach (var item in cartItems)
            {   
                RemoveFromCart(item.BookId);
            }

            _db.SaveChanges();

            return View("Confirmation");
        }

    }
}
