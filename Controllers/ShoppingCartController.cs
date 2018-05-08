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

        private BookService _bookService;
        private DataContext _db = new DataContext();

        public ShoppingCartController()
        {
            _cartRepo = new CartRepo();
            _bookService = new BookService();
        }

        public IActionResult Index(string searchString)
        {
            var cart = CartRepo.GetCart(this.HttpContext);

            var cartId = cart.ShoppingCartId;

            dynamic myModel = new ExpandoObject();
            
            var cartModel = new ShoppingCartViewModel
            {
                CartItems = _cartRepo.GetCartItems(cartId),
                CartTotal = _cartRepo.GetTotal(cartId)
            };

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
                            Price = items.Price,
                            Genre = items.Genre,
                            BoughtCopies = items.BoughtCopies,
                            Year = items.Year,
                            Description = items.Description,
                            Quantity = citems.Quantity
                        }).ToList();

            myModel.cartItems = cartModel;
            myModel.bookItems = books;

            return View(myModel);
        }

        [Authorize]
        public IActionResult AddToCart(int bookId)
        {
            var books = _bookService.GetAllBooks();

            var bookAdded = (from book in books
                            where book.Id == bookId
                            select book).SingleOrDefault();

            var cart = CartRepo.GetCart(this.HttpContext);


            _cartRepo.AddToCart(bookAdded, this.HttpContext);

            return RedirectToAction("Index", "Home");
        }
    }
}
