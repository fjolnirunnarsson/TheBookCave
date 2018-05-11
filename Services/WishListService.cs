using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Repositories;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

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

        public void AddToWishList(BookListViewModel book, WishList user)
        {
            _wishListRepo.AddToWishList(book, user);
        }

        public void RemoveFromWishList(BookListViewModel book, WishList user)
        {
            _wishListRepo.RemoveFromWishList(book, user);
        }

        public List<List> GetWishListItems(string userId)
        {
            return _wishListRepo.GetWishListItems(userId);
        }

        public List<BookListViewModel> GetWishListBooks(string userId)
        {
            return _wishListRepo.GetWishListBooks(userId);
        }
    }
}