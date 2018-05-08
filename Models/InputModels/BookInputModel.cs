using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Field required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage="Field required")]
        public string Author { get; set; }

        [Required(ErrorMessage="Field required")]
        public int Year { get; set; }

        [Required(ErrorMessage="Field required")]
        public double Price { get; set; }

        [Required(ErrorMessage="Field required")]
        public string Image { get; set; }

        [Required(ErrorMessage="Field required")]
        public string Genre { get; set; }
        public double Discount { get; set; }

        [RegularExpression(@"^\d+\.\d{1.2}")]
        public double DiscountPrice { get; set; }

        [Required(ErrorMessage="Field required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage="Field required")]
        public string Description { get; set; }
    }
}