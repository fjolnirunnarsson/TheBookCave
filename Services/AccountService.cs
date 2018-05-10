using System;
using System.Collections.Generic;
using TheBookCave.Data;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;
using Microsoft.AspNetCore.Http;

namespace TheBookCave.Services
{
    public class AccountService : IAccountService
    {
        private AccountRepo _accountRepo;
        private DataContext _db = new DataContext();

        public AccountService()
        {
            _accountRepo = new AccountRepo();
        }

        public List<AccountListViewModel> GetAllAccounts()
        {
            var accounts = _accountRepo.GetAllAccounts();

            return accounts;
        }

        public AccountListViewModel GetLoggedInAccount(string userId)
        {
            return _accountRepo.GetLoggedInAccount(userId);
        }
        public void ProcessAccount(AccountInputModel account)
        {
            if(string.IsNullOrEmpty(account.FirstName))
            {
                throw new Exception("First name is missing");
            }
            if(string.IsNullOrEmpty(account.LastName))
            {
                throw new Exception("Last name is missing");
            }
            if(string.IsNullOrEmpty(account.Email))
            {
                throw new Exception("Email is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressStreet))
            {
                throw new Exception("Street is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressHouseNumber))
            {
                throw new Exception("House number is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressCity))
            {
                throw new Exception("City is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressCountry))
            {
                throw new Exception("Country is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressZipCode))
            {
                throw new Exception("Postal code is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressStreet))
            {
                throw new Exception("Street is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressHouseNumber))
            {
                throw new Exception("House number is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressCity))
            {
                throw new Exception("City is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressCountry))
            {
                throw new Exception("Country is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressZipCode))
            {
                throw new Exception("Postal code is missing");
            }
        }

        public List<PurchasesViewModel> GetAllPurchases(string userId)
        {
            return _accountRepo.GetAllPurchases(userId);
        }

        public static string GetUser(HttpContext context)
        {
            var user = context.User.Identity.Name;
            return user;
        }

        public void UpdateAccount(string userId, AccountListViewModel model)
        {
            _accountRepo.UpdateAccount(userId, model);
        }
    }
}