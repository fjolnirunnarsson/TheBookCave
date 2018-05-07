namespace TheBookCave.Data.EntityModels
{
    public class Account
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address BillingAddress{ get; set; }
        public Address DeliveryAddress { get; set; }
        public string UserId { get; set; }
    }
}