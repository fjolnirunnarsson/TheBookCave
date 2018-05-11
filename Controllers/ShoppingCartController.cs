using System;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using TheBookCave.Data;
using TheBookCave.Services;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private CartService _cartService;
        private BookService _bookService;
        private AccountService _accountService;
        private WishListService _wishListService;
        private readonly IAccountService _IAccountService;
        private dynamic _myModel;
        private DataContext _db;

        public ShoppingCartController(IAccountService IAccountService)
        {
            _cartService = new CartService();
            _bookService = new BookService();
            _accountService = new AccountService();
            _wishListService = new WishListService();
            _IAccountService = IAccountService;
            _myModel = new ExpandoObject();
            _db = new DataContext();
        }

        public IActionResult Index(string searchString)
        {
            var user = WishListService.GetUser(this.HttpContext);
            var userId = user.UserId;
            var listModel = new WishListViewModel
            {
                ListItems = _wishListService.GetWishListItems(userId),
            };

            if (!String.IsNullOrEmpty(searchString))
            {
                var bookList = _bookService.GetSearchBooks(searchString);
                if (bookList.Count == 0)
                {
                    return View("NoResults");
                }

                _myModel.Book = bookList;
                _myModel.Account = listModel;

                return View("../Home/Index", _myModel);
            }

            var cart = CartService.GetCart(this.HttpContext);
            var cartId = cart.ShoppingCartId;
            var shoppingCart = new ShoppingCartViewModel
            {
                CartItems = _cartService.GetCartItems(cartId),
                CartTotal = _cartService.GetTotal(cartId)
            };

            var account = _accountService.GetAllAccounts();
            var books = _bookService.GetShoppingCartBooks(cartId);

            _myModel.cartItems = shoppingCart;
            _myModel.bookItems = books;
            _myModel.account = account;

            return View(_myModel);
        }

        [Authorize]
        public IActionResult AddToCart(int bookId)
        {
            var bookAdded = _bookService.GetBookById(bookId);
            var cart = CartService.GetCart(this.HttpContext);

            _cartService.AddToCart(bookAdded, this.HttpContext);

            string referer = Request.Headers["Referer"].ToString();

            return Redirect(referer);
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            var bookAdded = _bookService.GetBookById(bookId);
            var cart = CartService.GetCart(this.HttpContext);

            _cartService.RemoveFromCart(bookAdded, cart);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout(string email)
        {
            var account = _accountService.GetLoggedInAccount(email);
            return View(account);
        }

        [HttpPost]
        public IActionResult Checkout(AccountInputModel updatedAccount)
        {
            var user = HttpContext.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View();
            }

            _accountService.ProcessAccount(updatedAccount);
            _accountService.UpdateAccount(user, updatedAccount);

            return RedirectToAction("ReviewStep");
        }

        [HttpGet]
        public IActionResult ReviewStep()
        {
            var user = HttpContext.User.Identity.Name;
            var cart = CartService.GetCart(this.HttpContext);
            var cartId = cart.ShoppingCartId;

            var cartModel = new ShoppingCartViewModel
            {
                CartItems = _cartService.GetCartItems(cartId),
                CartTotal = _cartService.GetTotal(cartId)
            };

            var accounts = _accountService.GetAllAccounts();
            var accountmodel = _accountService.GetLoggedInAccount(user);

            _myModel.cartItems = cartModel;
            _myModel.account = accountmodel;

            return View(_myModel);
        }

        public IActionResult ConfirmationStep()
        {
            var user = HttpContext.User.Identity.Name;
            var cart = CartService.GetCart(this.HttpContext);

            _cartService.MoveToPurchased(user, cart);
            _cartService.ClearShoppingCart(user, cart);

            string email = user;
            _myModel.mail = email;

            return View("Confirmation", _myModel);
        }
    }
}
