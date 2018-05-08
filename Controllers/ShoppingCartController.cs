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

namespace TheBookCave.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private CartRepo _cartRepo;

        private BookService _bookService;
        private DataContext _db = new DataContext();

        public ShoppingCartController()
        {
            _cartRepo = new CartRepo();
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            var cart = CartRepo.GetCart(this.HttpContext);

            var cartId = cart.ShoppingCartId;

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = _cartRepo.GetCartItems(cartId),
                CartTotal = _cartRepo.GetTotal()
            };

            return View(cartViewModel);
        }

        [Authorize]
        public RedirectToActionResult AddToCart(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).Single();

            var cart = CartRepo.GetCart(this.HttpContext);


            _cartRepo.AddToCart(bookAdded);

            return RedirectToAction("Index");
        }
    }
}
