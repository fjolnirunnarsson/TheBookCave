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
            _accountRepo.ProcessAccount(account);
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