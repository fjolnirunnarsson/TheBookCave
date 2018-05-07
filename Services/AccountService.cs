using System.Collections.Generic;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

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
    }
}