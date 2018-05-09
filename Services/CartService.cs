using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.EntityModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

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

        public int RemoveFromCart(BookListViewModel book, HttpContext context)
        {
            return _cartRepo.RemoveFromCart(book, context);
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            return _cartRepo.GetCartItems(shoppingCartId);
        }

        public double GetTotal(string shoppingCartId)
        {
            return _cartRepo.GetTotal(shoppingCartId);
        }
    }
}