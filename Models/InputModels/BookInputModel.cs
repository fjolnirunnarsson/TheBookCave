using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Title is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage="Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage="Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage="Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage="Genre is required")]
        public string Genre { get; set; }
        public double Discount { get; set; }
        public double DiscountPrice { get; set; }

        [Required(ErrorMessage="Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage="Description is required")]
        public string Description { get; set; }
    }
}