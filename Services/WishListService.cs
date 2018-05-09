using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class WishListService
    {
        private WishListRepo _wishListRepo;

        public WishListService()
        {
            _wishListRepo = new WishListRepo();
        }

        public static WishList GetUser(HttpContext context)
        {
            var wishList = new WishList();
            wishList.UserId = context.User.Identity.Name;
            return wishList;
        }

        public void AddToWishList(BookListViewModel book, HttpContext context)
        {
            _wishListRepo.AddToWishList(book, context);
        }

        public void RemoveFromWishList(BookListViewModel book, HttpContext context)
        {
            _wishListRepo.RemoveFromWishList(book, context);
        }

        public List<List> GetWishListItems(string userId)
        {
            return _wishListRepo.GetWishListItems(userId);
        }
    }
}