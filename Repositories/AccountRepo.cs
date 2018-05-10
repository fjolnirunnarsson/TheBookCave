using System;
using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class AccountRepo
    {
        private DataContext _db = new DataContext();

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
                                DeliveryAddressStreet = a.DeliveryAddressStreet,
                                DeliveryAddressHouseNumber = a.DeliveryAddressHouseNumber,
                                DeliveryAddressLine2 = a.DeliveryAddressLine2,
                                DeliveryAddressCity = a.DeliveryAddressCity,
                                DeliveryAddressCountry = a.DeliveryAddressCountry,
                                DeliveryAddressZipCode = a.DeliveryAddressZipCode,
                            }).ToList();
            return accounts;
        }



        public AccountListViewModel GetLoggedInAccount(string userId)
        {
            var accounts = GetAllAccounts();
            var account = (from a in accounts
                           where a.Email == userId
                           select a).FirstOrDefault();
            return account;
        }

        public void UpdateAccount(string userId, AccountListViewModel model)
        {
            var accounts = GetAllAccounts();
            var account = (from a in _db.Accounts
                           where a.Email == model.Email
                           select a).FirstOrDefault();

            account.FirstName = model.FirstName;
            account.LastName = model.LastName;
            account.Email = model.Email;
            account.ProfilePicture = model.ProfilePicture;
            account.FavoriteBook = model.FavoriteBook;
            account.BillingAddressStreet = model.BillingAddressStreet;
            account.BillingAddressHouseNumber = model.BillingAddressHouseNumber;
            account.BillingAddressLine2 = model.BillingAddressLine2;
            account.BillingAddressCity = model.BillingAddressCity;
            account.BillingAddressCountry = model.BillingAddressCountry;
            account.BillingAddressZipCode = model.BillingAddressZipCode;

            if (model.SameAddresses == 1)
            {
                account.DeliveryAddressStreet = model.BillingAddressStreet;
                account.DeliveryAddressHouseNumber = model.BillingAddressHouseNumber;
                account.DeliveryAddressLine2 = model.BillingAddressLine2;
                account.DeliveryAddressCity = model.BillingAddressCity;
                account.DeliveryAddressCountry = model.BillingAddressCountry;
                account.DeliveryAddressZipCode = model.BillingAddressZipCode;
            }
            else
            {
                account.DeliveryAddressStreet = model.DeliveryAddressStreet;
                account.DeliveryAddressHouseNumber = model.DeliveryAddressHouseNumber;
                account.DeliveryAddressLine2 = model.DeliveryAddressLine2;
                account.DeliveryAddressCity = model.DeliveryAddressCity;
                account.DeliveryAddressCountry = model.DeliveryAddressCountry;
                account.DeliveryAddressZipCode = model.DeliveryAddressZipCode;
            }

            _db.SaveChanges();
        }

        public List<PurchasesViewModel> GetAllPurchases(string userId)
        {
            var purchased = (from item in _db.Books
                             join citems in _db.Purchased on item.Id equals citems.BookId
                             where citems.CartId == userId
                             select new PurchasesViewModel
                             {
                                 Id = item.Id,
                                 Image = item.Image,
                                 Title = item.Title,
                                 Author = item.Author,
                                 Rating = item.Rating,
                                 Price = item.DiscountPrice,
                                 Quantity = citems.Quantity,
                                 DateCreated = citems.DateCreated
                             }).ToList();
            return purchased;
        }
<<<<<<< HEAD

        public void ProcessAccount(AccountInputModel account)
        {
            if (string.IsNullOrEmpty(account.FirstName)) { throw new Exception("First name is missing"); }
            if (string.IsNullOrEmpty(account.LastName)) { throw new Exception("Last name is missing"); }
            if (string.IsNullOrEmpty(account.Email)) { throw new Exception("Email is missing"); }
            if (string.IsNullOrEmpty(account.BillingAddressStreet)) { throw new Exception("Street is missing"); }
            if (string.IsNullOrEmpty(account.BillingAddressHouseNumber)) { throw new Exception("House number is missing"); }
            if (string.IsNullOrEmpty(account.BillingAddressCity)) { throw new Exception("City is missing"); }
            if (string.IsNullOrEmpty(account.BillingAddressCountry)) { throw new Exception("Country is missing"); }
            if (string.IsNullOrEmpty(account.BillingAddressZipCode)) { throw new Exception("Postal code is missing"); }
            if (string.IsNullOrEmpty(account.DeliveryAddressStreet)) { throw new Exception("Street is missing"); }
            if (string.IsNullOrEmpty(account.DeliveryAddressHouseNumber)) { throw new Exception("House number is missing"); }
            if (string.IsNullOrEmpty(account.DeliveryAddressCity)) { throw new Exception("City is missing"); }
            if (string.IsNullOrEmpty(account.DeliveryAddressCountry)) { throw new Exception("Country is missing"); }
            if (string.IsNullOrEmpty(account.DeliveryAddressZipCode)) { throw new Exception("Postal code is missing"); }
        }
=======
        
>>>>>>> 763167de2ef17c9b11e89930cf3c6db870fe3670
    }
}