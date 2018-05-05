using System;
using System.Collections.Generic;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

/*namespace TheBookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;
        public ShoppingCart cart { get; set; }
        public List<CartItem> CartItems { get; set; }

        public CartService() 
        {
            _cartRepo = new CartRepo();
        }
        public void AddToCart(BookListViewModel book, int quantity)
        {
            _cartRepo.AddToCart(book, quantity);
        }

        public void RemoveFromCart(BookListViewModel book)
        {
            _cartRepo.RemoveFromCart(book);
        }

        public List<CartItem> GetCartItems()
        {
            return _cartRepo.GetCartItems();
        }

        public double GetCartTotal()
        {
            return _cartRepo.GetCartTotal();
        }
    }
}*/