using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class AccountRepo
    {
        private DataContext _db;

        public AccountRepo()
        {
            _db = new DataContext();
        }

        
        public List<AccountListViewModel> GetAllAccounts()
        {
            
            var accounts = (from a in _db.Accounts
                            select new AccountListViewModel
                           {
                                FirstName = a.FirstName,
                                LastName = a.LastName,
                                Email = a.Email,
                                ProfilePicture = a.ProfilePicture,
                                FavoriteBook = a.FavoriteBook,
                                BillingAddressStreet = a.BillingAddressStreet,
                                BillingAddressHouseNumber = a.BillingAddressHouseNumber,
                                BillingAddressLine2 = a.BillingAddressLine2, 
                                BillingAddressCity = a.BillingAddressCity,
                                BillingAddressCountry = a.BillingAddressCountry,
                                BillingAddressZipCode = a.BillingAddressZipCode,
                                DeliveryAddressStreet  = a.DeliveryAddressStreet,
                                DeliveryAddressHouseNumber = a.DeliveryAddressHouseNumber,
                                DeliveryAddressLine2 = a.DeliveryAddressLine2,
                                DeliveryAddressCity = a.DeliveryAddressCity,
                                DeliveryAddressCountry = a.DeliveryAddressCountry,
                                DeliveryAddressZipCode = a.DeliveryAddressZipCode,
                           }).ToList();
            return accounts;
        }

        public static string GetUser(HttpContext context)
        {
            var user = context.User.Identity.Name;
            return user;
        }
        
        public List<BookListViewModel> GetAllPurchases(HttpContext context)
        {
            var user = GetUser(context);
            var purchased = (from item in _db.Books
                            join citems in _db.Purchased on item.Id equals citems.BookId 
                            where citems.CartId == user
                            select new BookListViewModel
                            {
                                Id = item.Id,
                                Image = item.Image,
                                Title = item.Title,
                                Author = item.Author,
                                AuthorId = item.AuthorId,
                                Rating = item.Rating,
                                Price = item.DiscountPrice,
                                Genre = item.Genre,
                                BoughtCopies = item.BoughtCopies,
                                Year = item.Year,
                                Description = item.Description,
                                Quantity = citems.Quantity,
                            }).ToList();
                            return purchased;
        }
    }
}