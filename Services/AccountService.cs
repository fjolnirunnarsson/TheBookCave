using System.Collections.Generic;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;
using Microsoft.AspNetCore.Http;

namespace TheBookCave.Services
{
    public class AccountService
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

        public List<BookListViewModel> GetAllPurchases(HttpContext context)
        {
            
            return _accountRepo.GetAllPurchases(context);
        }

        public static string GetUser(HttpContext context)
        {
            var user = context.User.Identity.Name;
            return user;
        }
    }
}