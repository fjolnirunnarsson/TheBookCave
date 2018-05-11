using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class WishListRepo
    {
        private BookRepo _bookRepo;
        private DataContext _db;
        public WishListRepo()
        {
            _bookRepo = new BookRepo();
            _db = new DataContext();
        }

        public WishList GetUser(HttpContext context)
        {
            var wishList = new WishList();
            wishList.UserId = context.User.Identity.Name;
            return wishList;
        }

        public void AddToWishList(BookListViewModel book, WishList user)
        {
            var listItem = (from item in _db.Lists
                            where item.Book.Id == book.Id && item.UserId == user.UserId
                            select item).SingleOrDefault();

            if (listItem == null)
            {
                listItem = new List
                {
                    BookId = book.Id,
                    UserId = user.UserId,
                };

                _db.Lists.Add(listItem);
            }
            _db.SaveChanges();
        }

        public void RemoveFromWishList(BookListViewModel book, WishList user)
        {
            var listItem = (from item in _db.Lists
                            where item.Book.Id == book.Id && item.UserId == user.UserId
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

        public List<BookListViewModel> GetWishListBooks(string userId)
        {
            var books = (from items in _db.Books
                         join citems in _db.Lists on items.Id equals citems.BookId
                         where citems.UserId == userId
                         select new BookListViewModel
                         {
                             Id = items.Id,
                             Image = items.Image,
                             Title = items.Title,
                             Author = items.Author,
                             AuthorId = items.AuthorId,
                             Rating = items.Rating,
                             Price = items.DiscountPrice,
                             Genre = items.Genre,
                             BoughtCopies = items.BoughtCopies,
                             Description = items.Description,
                         }).ToList();
            return books;
        }

    }
}