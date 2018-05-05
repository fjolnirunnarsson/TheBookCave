using System.ComponentModel.DataAnnotations;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Data.EntityModels
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public BookListViewModel BookItem { get; set; }
    }
}