namespace TheBookCave.Models.ViewModels
{
    public class PurchasesViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}

