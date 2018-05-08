using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
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
                                DeliveryAddressZipCode = a.DeliveryAddressZipCode
                                
                           }).ToList();
            return accounts;
        }
    }
}