namespace TheBookCave.Data.EntityModels
{
    public class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string CostumerName { get; set; }
        public int BookId { get; set; }
    }
}