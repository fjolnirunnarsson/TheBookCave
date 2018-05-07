using System.Collections.Generic;
using TheBookCave.Data.EntityModels;

namespace TheBookCave.Models.ViewModels
{
    public class BookListViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public int BoughtCopies { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<Review> Reviews  { get; set; }
    }
}