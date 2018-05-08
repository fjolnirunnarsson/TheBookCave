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
using TheBookCave.Repositories;
using TheBookCave.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TheBookCave.Models.EntityModels;

namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db = new DataContext();

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = context.User.Identity.Name;
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(BookListViewModel book)
        {
            var cart = new ShoppingCart();
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == cart.ShoppingCartId
                && c.BookId == book.Id);
            
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    BookId = book.Id,
                    CartId = cart.ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return _db.Carts.Where(
                cart => cart.CartId == shoppingCartId).ToList();
                        
        }

        public decimal GetTotal()
        {
            var newCart = new ShoppingCart();
            decimal? total = (from cartItems in _db.Carts
                                where cartItems.CartId == newCart.ShoppingCartId
                                select (int?)cartItems.Quantity *
                                (decimal)cartItems.Book.Price).Sum();
            return total ?? decimal.Zero;
        }
    }
}