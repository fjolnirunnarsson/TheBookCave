using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Data.EntityModels
{
    public class Order
    {
        public int OrderId { get; set; }
        //public System.DateTime OrderDate { get; set }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Experation { get; set; }

        [DataType(DataType.CreditCard)]
        public String CreditCard { get; set; }

        public String CcType { get; set; }

        public double Total { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    
    }
}