using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.ViewModels
{
    public class AccountListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string Email { get; set; }
        public string BillingAddressStreet { get; set; }
        public int BillingAddressHouseNumber { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressCountry { get; set; }
        public string BillingAddressZipCode { get; set; }
        public string DeliveryAddressStreet { get; set; }
        public int DeliveryAddressHouseNumber { get; set; }
        public string DeliveryAddressLine2 { get; set; }
        public string DeliveryAddressCity { get; set; }
        public string DeliveryAddressCountry { get; set; }
        public string DeliveryAddressZipCode { get; set; }
    }
}