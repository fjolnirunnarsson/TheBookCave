namespace TheBookCave.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
    }
}