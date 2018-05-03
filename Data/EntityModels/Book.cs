namespace TheBookCave.Data.EntityModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public int BoughtCopies { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}