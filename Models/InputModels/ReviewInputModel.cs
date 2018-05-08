namespace TheBookCave.Models.InputModels
{
    public class ReviewInputModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
    }
}