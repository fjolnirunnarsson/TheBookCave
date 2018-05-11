using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class AccountInputModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public string FavoriteBook { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string BillingAddressStreet { get; set; }

        [Required(ErrorMessage = "House number is required")]
        public string BillingAddressHouseNumber { get; set; }
        public string BillingAddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string BillingAddressCity { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string BillingAddressCountry { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        public string BillingAddressZipCode { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string DeliveryAddressStreet { get; set; }

        [Required(ErrorMessage = "House number is required")]
        public string DeliveryAddressHouseNumber { get; set; }
        public string DeliveryAddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string DeliveryAddressCity { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string DeliveryAddressCountry { get; set; }

        [Required(ErrorMessage = "Postal code required")]
        public string DeliveryAddressZipCode { get; set; }
        public int SameAddresses { get; set; }
    }
}