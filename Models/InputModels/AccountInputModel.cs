using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class AccountInputModel
    {

        [Required(ErrorMessage="Field required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="Field required")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Field required")]
        public string Email { get; set; }

        [Required(ErrorMessage="Field required")]
        public string BillingAddressStreet { get; set; }

        [Required(ErrorMessage="Field required")]
        public int BillingAddressHouseNumber { get; set; }
        public string BillingAddressLine2 { get; set; }

        [Required(ErrorMessage="Field required")]
        public string BillingAddressCity { get; set; }

        [Required(ErrorMessage="Field required")]
        public string BillingAddressCountry { get; set; }

        [Required(ErrorMessage="Field required")]
        public string BillingAddressZipCode { get; set; }

        [Required(ErrorMessage="Field required")]
        public string DeliveryAddressStreet { get; set; }

        [Required(ErrorMessage="Field required")]
        public int DeliveryAddressHouseNumber { get; set; }
        public string DeliveryAddressLine2 { get; set; }

        [Required(ErrorMessage="Field required")]
        public string DeliveryAddressCity { get; set; }

        [Required(ErrorMessage="Field required")]
        public string DeliveryAddressCountry { get; set; }

        [Required(ErrorMessage="Field required")]
        public string DeliveryAddressZipCode { get; set; }

    }
}