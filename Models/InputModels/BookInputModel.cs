using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="The book needs a title!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage="The book needs an author!")]
        public string Author { get; set; }

        [Required(ErrorMessage="The book needs a price!")]
        public double Price { get; set; }
        public int Discount { get; set; }

        [Required(ErrorMessage="How many copies are there?")]
        public int Quantity { get; set; }

        [Required(ErrorMessage="The book needs a description!")]
        public string Description { get; set; }

        [Required(ErrorMessage="The book needs a cover!")]
        public string Image { get; set; }
    }
}