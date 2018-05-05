using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Models.ViewModels
{
    public class ShoppingCart
    {
        private readonly DataContext _db;
        DataContext db = new DataContext();
        
        private ShoppingCart(DataContext db)
        {
            _db = db;
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

        internal static object GetCart()
        {
            throw new NotImplementedException();
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

        public void MigrateCart(string username)
        {
            var shoppingCart = db.CartItems.Where(
                c => c.CartId == ShoppingCartId);
            
            foreach (CartItem item in shoppingCart)
            {
                item.CartId = username;
            }
 
            db.SaveChanges(); 
            
        }
    }
}