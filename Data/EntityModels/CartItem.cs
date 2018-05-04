namespace TheBookCave.Data.EntityModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        //public int BookId { get; set; }
        public int Count { get; set; }
        public Book BookItem { get; set; }
    }
}