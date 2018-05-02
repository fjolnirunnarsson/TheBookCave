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
                new BookListViewModel { Id = 1, Image = "https://img1.od-cdn.com/ImageType-400/1523-1/CE6/AD9/B9/%7BCE6AD9B9-6435-45E8-9A0E-74DCB44E6E4F%7DImg400.jpg", Title = "Cuckoo's Nest", Author = "Ken Kesey", Rating = 4.3, Price = 23.99, Category = "Fiction" },
                new BookListViewModel { Id = 1, Image = "https://images-na.ssl-images-amazon.com/images/I/41MeC94AxIL._SX324_BO1,204,203,200_.jpg", Title = "The Alchemist", Author = "Paul Coelho", Rating = 4.7, Price = 19.99, Category = "Philosophy" },
                new BookListViewModel { Id = 1, Image = "https://www.forlagid.is/wp-content/uploads/2017/04/Stofuhiti_72.jpg", Title = "Stofuhiti", Author = "Bergur Ebbi", Rating = 4.1, Price = 14.99, Category = "Philosophy" },
                new BookListViewModel { Id = 1, Image = "https://img1.od-cdn.com/ImageType-400/1523-1/CE6/AD9/B9/%7BCE6AD9B9-6435-45E8-9A0E-74DCB44E6E4F%7DImg400.jpg", Title = "Cuckoo's Nest", Author = "Ken Kesey", Rating = 4.3, Price = 23.99, Category = "Fiction" },
                new BookListViewModel { Id = 1, Image = "https://images-na.ssl-images-amazon.com/images/I/41MeC94AxIL._SX324_BO1,204,203,200_.jpg", Title = "The Alchemist", Author = "Paul Coelho", Rating = 4.7, Price = 19.99, Category = "Philosophy" },
                new BookListViewModel { Id = 1, Image = "https://www.forlagid.is/wp-content/uploads/2017/04/Stofuhiti_72.jpg", Title = "Stofuhiti", Author = "Bergur Ebbi", Rating = 4.1, Price = 14.99, Category = "Philosophy" },
                new BookListViewModel { Id = 1, Image = "https://img1.od-cdn.com/ImageType-400/1523-1/CE6/AD9/B9/%7BCE6AD9B9-6435-45E8-9A0E-74DCB44E6E4F%7DImg400.jpg", Title = "Cuckoo's Nest", Author = "Ken Kesey", Rating = 4.3, Price = 23.99, Category = "Fiction" },
                new BookListViewModel { Id = 1, Image = "https://images-na.ssl-images-amazon.com/images/I/41MeC94AxIL._SX324_BO1,204,203,200_.jpg", Title = "The Alchemist", Author = "Paul Coelho", Rating = 4.7, Price = 19.99, Category = "Philosophy" },
                new BookListViewModel { Id = 1, Image = "https://www.forlagid.is/wp-content/uploads/2017/04/Stofuhiti_72.jpg", Title = "Stofuhiti", Author = "Bergur Ebbi", Rating = 4.1, Price = 14.99, Category = "Philosophy" }
            }; 
            return books;
        }
    }
}