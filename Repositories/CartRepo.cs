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
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db = new DataContext();

        private BookRepo _bookRepo = new BookRepo();

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

        public void AddToCart(BookListViewModel book, HttpContext context)
        {
            var cart = GetCart(context);

            var cartItem = (from item in _db.Carts
                        where item.Book.Id == book.Id && item.CartId == cart.ShoppingCartId
                        select item).SingleOrDefault();
            
            
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
            var cartItems = (from item in _db.Carts
                            where item.CartId == shoppingCartId
                            select item).ToList();
            return cartItems;
                        
        }

        public double GetTotal(string shoppingCartId)
        {
            var total = (from items in _db.Carts
                        where items.CartId == shoppingCartId
                        select items.Quantity * items.Book.Price).Sum();
            
            return total;
        }
    }
}