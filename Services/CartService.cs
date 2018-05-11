using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data;
using TheBookCave.Repositories;
using TheBookCave.Models.ViewModels;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.EntityModels;

namespace TheBookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;

        public CartService()
        {
            _cartRepo = new CartRepo();
        }

        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = context.User.Identity.Name;
            return cart;
        }

        public void AddToCart(BookListViewModel book, HttpContext context)
        {
            _cartRepo.AddToCart(book, context);
        }

        public void RemoveFromCart(BookListViewModel book, ShoppingCart cart)
        {
            _cartRepo.RemoveFromCart(book, cart);
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return _cartRepo.GetCartItems(shoppingCartId);
        }

        public double GetTotal(string shoppingCartId)
        {
            return _cartRepo.GetTotal(shoppingCartId);
        }

        public void MoveToPurchased(string user, ShoppingCart cart)
        {
            _cartRepo.MoveToPurchased(user, cart);
        }

        public void ClearShoppingCart(string user, ShoppingCart cart)
        {
            _cartRepo.ClearShoppingCart(user, cart);
        }
    }
}