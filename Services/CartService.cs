using System.Collections.Generic;
using TheBookCave.Data.EntityModels;
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

        public ShoppingCartViewModel CreateFakeCart() 
        {
            var fakeCart = _cartRepo.CreateFakeCart();
            return fakeCart;
        }
    }
}