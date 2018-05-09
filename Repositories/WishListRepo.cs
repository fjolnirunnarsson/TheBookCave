using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;
using System.Linq;

namespace TheBookCave.Repositories
{
    public class WishListRepo
    {
        private DataContext _db = new DataContext();

        private BookRepo _bookRepo = new BookRepo();

        public WishList GetUser(HttpContext context)
        {
            var wishList = new WishList();
            wishList.UserId = context.User.Identity.Name;
            return wishList;
        }

        public void AddToWishList(BookListViewModel book, HttpContext context)
        {
            var wishList = GetUser(context);

            var listItem = (from item in _db.Lists
                        where item.Book.Id == book.Id && item.UserId == wishList.UserId
                        select item).SingleOrDefault();

            if (listItem == null)
            {
                listItem = new List
                {
                    BookId = book.Id,
                    UserId = wishList.UserId,
                };
                _db.Lists.Add(listItem);
            
            }
            _db.SaveChanges();
        }

        public void RemoveFromWishList(BookListViewModel book, HttpContext context)
        {
        var wishList = GetUser(context);

        var listItem = (from item in _db.Lists
                    where item.Book.Id == book.Id && item.UserId == wishList.UserId
                    select item).SingleOrDefault();
        
    
        
            if (listItem != null)
            {

                _db.Lists.Remove(listItem);
    
            }
            _db.SaveChanges();
        }

        public List<List> GetWishListItems(string userId)
        {
            var listItems = (from item in _db.Lists
                            where item.UserId == userId
                            select item).ToList();
            return listItems;
                        
        }

    }
}