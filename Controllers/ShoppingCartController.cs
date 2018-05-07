using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;
using TheBookCave.Services;

/*namespace TheBookCave.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly BookService _BookService;
        private readonly ShoppingCart _shoppingCart;


        public ShoppingCartController(BookService BookService, ShoppingCart shoppingCart)
        {
            _BookService = BookService;
            _shoppingCart = shoppingCart;
        } 
        public IActionResult Index()
        {
            var items = _shoppingCart.GetCartItems();
            _shoppingCart.CartItems = items;

            var cartModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                CartTotal = _shoppingCart.GetCartTotal()
            };
            
            return View(cartModel);
        }

        public RedirectToActionResult AddToCart(int bookId)  
        {
            var books = _BookService.GetAllBooks();
            var selectedBook = books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null)
            {
                _shoppingCart.AddToCart(selectedBook, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int bookId)
        {
            var books = _BookService.GetAllBooks();
            var selectedBook = books.FirstOrDefault(b => b.Id == bookId);
            if (selectedBook != null) 
            {
                _shoppingCart.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
        }
    }
}
*/