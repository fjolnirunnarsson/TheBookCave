using System;
using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        private readonly DataContext _db;
        private ReviewRepo _reviewRepo = new ReviewRepo();

        public BookRepo()
        {
            _db = new DataContext();
        }
        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from a in _db.Books
                        select new BookListViewModel
                        {
                            Id = a.Id,
                            Image = a.Image,
                            Title = a.Title,
                            Author = a.Author,
                            AuthorId = a.AuthorId,
                            Rating = a.Rating,
                            Price = a.Price,
                            Genre = a.Genre,
                            BoughtCopies = a.BoughtCopies,
                            Description = a.Description,
                            Quantity = a.Quantity,
                            Discount = a.Discount,
                            DiscountPrice = System.Math.Round(((1 - a.Discount/100) * a.Price),2)
                        }).ToList();
            
            return books;
        }
        public List<BookListViewModel> GetSearchBooks(string searchString)
        {
            var books = GetAllBooks();

            var bookList = (from b in books
                            where b.Title.ToLower().Contains(searchString.ToLower())
                            || b.Author.ToLower().Contains(searchString.ToLower())
                            select b).ToList();
            return bookList;
        }

        public List<BookListViewModel> GetTop10()
        {
            var books = GetAllBooks();

            var top10 = (from book in books
                        orderby book.Rating descending
                        select book).Take(10).ToList();

            return top10;
        }               
        public BookListViewModel GetBookByTitle(string title)
        {
            var books = GetAllBooks();

            var book = (from item in books
                        where (item.Title).ToLower() == (title).ToLower()
                        select item).FirstOrDefault();
            return book;
        }

        public BookListViewModel GetBookById(int id)
        {
            var books = GetAllBooks();

            var book = (from b in books
                         where b.Id == id
                         select b).SingleOrDefault();
            return book;
        }
        public List<ReviewViewModel> GetAllReviews()
        {
            var reviews = _reviewRepo.GetAllReviews();
            return reviews;
        }
        public List<ReviewViewModel> GetBookReviews(string title)
        {
            var book = GetBookByTitle(title);
            var reviews = GetAllReviews();

            var bookReviews = (from rev in reviews
                            where rev.BookId == book.Id
                            select rev).ToList();
            
            return bookReviews;
        }

        public List<BookListViewModel> GetBooksByGenre(string genre)
        {
            var books = GetAllBooks();
            var genreList = (from item in books
                                where item.Genre.ToLower() == genre.ToLower()
                                orderby item.Title ascending
                                select item).ToList();
            return genreList;
        }
        public List<BookListViewModel> GetBooksByNewest()
        {
            var books = GetAllBooks();
            var newest = (from b in books
                            orderby b.Id descending
                            select b).ToList();
            return newest;
        }

        public List<BookListViewModel> GetBooksTitleOrder()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            orderby b.Title ascending
                            select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksOnSale()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            select b).ToList();
            return orderedBooks;
        }

        public List<BookListViewModel> GetBooksOnSaleAZ()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Title ascending
                            select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksOnSaleLH()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Price ascending
                            select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksOnSaleHL()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Price descending
                            select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksOnSaleNewest()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            where b.Price != b.DiscountPrice
                            orderby b.Id descending
                            select b).ToList();
            return orderedBooks;
        }

        public List<BookListViewModel> GetBooksByAuthorAZ()
        {
            var books = GetAllBooks();
            var booklist = (from b in books
                orderby b.Author ascending
                select b).ToList();
            return booklist;
        }

        public List<BookListViewModel> GetBooksPriceOrderLH()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            orderby b.Price ascending
                            select b).ToList();
            return orderedBooks;
        }

        public List<BookListViewModel> GetBooksPriceOrderHL()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                            orderby b.Price descending
                            select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksGenreOrderAZ()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                orderby b.Genre ascending
                select b).ToList();
            return orderedBooks;
        }


        public void SeedDataCreateReview(ReviewInputModel review, string user)
        {   
            var Reviews = new List<Review>{

                new Review{
                    Rating = review.Rating,
                    Comment = review.Comment,
                    UserName = user,
                    BookId = review.BookId
                }
            };
            _db.AddRange(Reviews);
            _db.SaveChanges();
        }
        public void UpdateBookRating(ReviewInputModel review)
        {
            var book = (from item in _db.Books
                        where item.Id == review.BookId
                        select item).FirstOrDefault();
                            
            var allReviews = (from item in _db.Reviews
                        where item.BookId == book.Id
                        select item).ToList();

            book.Rating = Math.Round(GetRating(allReviews),2);
            _db.SaveChanges();
        }        
        public double GetRating(List<Review> reviews)
        {
            double rating = 0;
            foreach(var review in reviews){
                rating += review.Rating;
            }
            rating = rating/reviews.Count();
            return rating;
        }

        public BookListViewModel GetBookByReview(ReviewInputModel review)
        {
            var books = GetAllBooks();
            var book = (from b in books
                        where b.Id == review.BookId
                        select b).FirstOrDefault();
            return book;
        }
        public List<BookListViewModel> GetBooksByDiscount()
        {
            var books = GetAllBooks();
            var orderedBooks = (from b in books
                orderby b.Discount descending
                select b).ToList();
            return orderedBooks;
        }
        public List<BookListViewModel> GetBooksByQuantity()
        {
            var books = GetAllBooks();
            var booklist = (from b in books
                orderby b.Quantity ascending
                select b).ToList();
            return booklist;
        }
        public List<BookListViewModel> GetBooksOrderSold()
        {
            var books = GetAllBooks();
            var booklist = (from b in books
                orderby b.BoughtCopies descending
                select b).ToList();
            return booklist;
        }

        public void SeedDataCreateAccount(RegisterViewModel model)
        {
            var Accounts = new List<Account>
            {
                new Account
                {
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                    Email = model.Email
                }
            };
            
            _db.AddRange(Accounts);
            _db.SaveChanges();
        }

        public void SeedDataCreateBook(BookInputModel book)
        {
                var newBook = new Book()
                {
                    Title = book.Title, 
                    Author = book.Author, 
                    Description = book.Description,
                    Image = book.Image, 
                    Genre = book.Genre,  
                    Price = book.Price, 
                    Discount = book.Discount, 
                    Quantity = book.Quantity, 
                };
                
            _db.Add(newBook);
            _db.SaveChanges();
        }
        public void SeedDataChangeBook(BookInputModel updatedBook)
        {

            var book = (from b in _db.Books
                where b.Id == updatedBook.Id
                select b).FirstOrDefault();

            book.Title = updatedBook.Title;
            book.Image = updatedBook.Image;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Quantity = updatedBook.Quantity;
            book.Price = updatedBook.Price;
            book.Description = updatedBook.Description;
            book.Discount = updatedBook.Discount;
            book.DiscountPrice = System.Math.Round((1 - updatedBook.Discount/100) * updatedBook.Price,2);
            
            _db.SaveChanges();
        }
    }
}