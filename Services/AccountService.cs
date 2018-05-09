using System;
using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class AccountService : IAccountService
    {
        private AccountRepo _accountRepo;

        public AccountService()
        {
            _accountRepo = new AccountRepo();
        }

        public List<AccountListViewModel> GetAllAccounts()
        {
            var accounts = _accountRepo.GetAllAccounts();

            return accounts;
        }

        public void ProcessAccount(AccountInputModel account)
        {
            if(string.IsNullOrEmpty(account.FirstName)) { throw new Exception("First name is missing"); }
            if(string.IsNullOrEmpty(account.LastName)) { throw new Exception("Last name is missing"); }
            if(string.IsNullOrEmpty(account.Email)) { throw new Exception("Email is missing"); }
            if(string.IsNullOrEmpty(account.BillingAddressStreet)) { throw new Exception("Street is missing"); }
            if(string.IsNullOrEmpty(account.BillingAddressHouseNumber)) { throw new Exception("House number is missing"); }
            if(string.IsNullOrEmpty(account.BillingAddressCity)) { throw new Exception("City is missing"); }
            if(string.IsNullOrEmpty(account.BillingAddressCountry)) { throw new Exception("Country is missing"); }
            if(string.IsNullOrEmpty(account.BillingAddressZipCode)) { throw new Exception("Postal code is missing"); }
            if(string.IsNullOrEmpty(account.DeliveryAddressStreet)) { throw new Exception("Street is missing"); }
            if(string.IsNullOrEmpty(account.DeliveryAddressHouseNumber)) { throw new Exception("House number is missing"); }
            if(string.IsNullOrEmpty(account.DeliveryAddressCity)) { throw new Exception("City is missing"); }
            if(string.IsNullOrEmpty(account.DeliveryAddressCountry)) { throw new Exception("Country is missing"); }
            if(string.IsNullOrEmpty(account.DeliveryAddressZipCode)) { throw new Exception("Postal code is missing"); }

        }
    }
}