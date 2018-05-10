using System;
using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public interface IAccountService
    {
        void ProcessAccount(AccountInputModel account);
    }
}