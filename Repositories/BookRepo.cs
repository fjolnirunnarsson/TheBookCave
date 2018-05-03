using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from a in _db.Books
                        select new BookListViewModel
                        {
                            Image = a.Image,
                            Title = a.Title,
                            Author = a.Author,
                            Rating = a.Rating,
                            Price = a.Price,
                            Genre = a.Genre,
                            Description = a.Description
                            
                        }).ToList();
            
            return books;
        }
    }
}