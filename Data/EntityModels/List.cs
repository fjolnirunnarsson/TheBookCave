using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Data.EntityModels
{
    public class List
    {
        [Key]
        public int RecordId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}