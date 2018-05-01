using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        /*private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }*/

        public List<BookListViewModel> GetAllBooks()
        {
            /*var books = (from a in _db.Books
                        join ar in _db.Authors on a.AuthorId equals ar.Id
                        select new BookListViewModel
                        {
                            BookId = a.Id,
                            Title = a.Title,
                            Author = a.Author,
                            AuthorId = a.Id
                        }).ToList();
            */
            var books = new List<BookListViewModel>
            {
                new BookListViewModel { BookId = 1, Title = "Cuckoo's Nest", Author = "Ernest Hemingway", AuthorId = 1 },
                new BookListViewModel { BookId = 2, Title = "The Alchemist", Author = "Paul Coelho", AuthorId = 2 },
                new BookListViewModel { BookId = 3, Title = "Harry Potter", Author = "J.K. Rowling", AuthorId = 3 }
            }; 
            return books;
        }
    }
}