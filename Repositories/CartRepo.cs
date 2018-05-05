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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

/*namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        /*private readonly DataContext _db;

        public CartRepo()
        {

        }
        public string ShoppingCartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            
            var context = services.GetService<DataContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(BookListViewModel book, int amount)
        {

            var cartItem = _db.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.BookItem.Id == book.Id);
                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        ItemId = ShoppingCartId,
                        CartId = ShoppingCartId,
                        BookItem = book,
                        Quantity = 1,
                        DateCreated = DateTime.Now
                    };

                    _db.CartItems.Add(cartItem);
                }
                else 
                {
                    cartItem.Quantity++;
                }
                _db.SaveChanges();   
        }

        public int RemoveFromCart(BookListViewModel book)
        {
            var cartItem = _db.CartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId 
                && c.BookItem.Id == book.Id);

            var localQuantity = 0;

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                localQuantity = cartItem.Quantity;
            }
            else
            {
                _db.CartItems.Remove(cartItem);
            }
            _db.SaveChanges();
            return localQuantity;
        }

        public List<CartItem> GetCartItems()
        {
            return CartItems ?? (CartItems = _db.CartItems.Where(c => c.CartId == ShoppingCartId)
                .Include(c => c.BookItem).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _db.CartItems
                .Where(c => c.CartId == ShoppingCartId);

            _db.CartItems.RemoveRange(cartItems);
            _db.SaveChanges(); 
        }

        public double GetCartTotal()
        {
            var total = _db.CartItems.Where(c => c.CartId == ShoppingCartId)
                .Select(c => c.BookItem.Price * c.Quantity).Sum();

            return total;
        }
    }*/