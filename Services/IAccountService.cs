using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class IAccountService
    {
        public void ProcessContact(AccountInputModel account)
        {
            if(string.IsNullOrEmpty(account.FirstName))
            {
                throw new KeyNotFoundException("First name is missing");
            }
            if(string.IsNullOrEmpty(account.LastName))
            {
                throw new KeyNotFoundException("Last name is missing");
            }
            if(string.IsNullOrEmpty(account.Email))
            {
                throw new KeyNotFoundException("Email is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressStreet))
            {
                throw new KeyNotFoundException("Street is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressHouseNumber))
            {
                throw new KeyNotFoundException("House number is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressCity))
            {
                throw new KeyNotFoundException("City is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressCountry))
            {
                throw new KeyNotFoundException("Country is missing");
            }
            if(string.IsNullOrEmpty(account.BillingAddressZipCode))
            {
                throw new KeyNotFoundException("Postal code is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressStreet))
            {
                throw new KeyNotFoundException("Street is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressHouseNumber))
            {
                throw new KeyNotFoundException("House number is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressCity))
            {
                throw new KeyNotFoundException("City is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressCountry))
            {
                throw new KeyNotFoundException("Country is missing");
            }
            if(string.IsNullOrEmpty(account.DeliveryAddressZipCode))
            {
                throw new KeyNotFoundException("Postal code is missing");
            }
        }
    }
}