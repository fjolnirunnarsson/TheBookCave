using System.Collections.Generic;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Models.ViewModels
{
    public class ShoppingCart
    {
        public List<CartItem> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}