using System;
using System.Collections.Generic;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class BookService : IBookService
    {
        private BookRepo _bookRepo;
        private ReviewRepo _reviewRepo;
        public BookService() 
        {
            _bookRepo = new BookRepo();
            _reviewRepo = new ReviewRepo();
        }
        public List<BookListViewModel> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            
            return books;
        }
        public List<ReviewViewModel> GetAllReviews(){
            
            var reviews = _reviewRepo.GetAllReviews();

            return reviews;
        }
        public List<BookListViewModel> GetSearchBooks(string searchString)
        {
            return _bookRepo.GetSearchBooks(searchString);
        }
        public List<BookListViewModel> GetBooksByNewest()
        {
            return _bookRepo.GetBooksByNewest();
        }
        public List<BookListViewModel> GetTop10()
        {
            return _bookRepo.GetTop10();
        }
        public BookListViewModel GetBookByTitle(string title)
        {
            return _bookRepo.GetBookByTitle(title);
        }
        public List<BookListViewModel> GetBooksTitleOrder()
        {
            return _bookRepo.GetBooksTitleOrder();
        }
        public List<BookListViewModel> GetBooksPriceOrderLH()
        {
            return _bookRepo.GetBooksPriceOrderLH();
        }
        public List<BookListViewModel> GetBooksPriceOrderHL()
        {
            return _bookRepo.GetBooksPriceOrderHL();
        }
        public List<ReviewViewModel> GetBookReviews(string title)
        {
            return _bookRepo.GetBookReviews(title);
        }
        public void SeedDataCreate(ReviewInputModel review, string user)
        {
            _bookRepo.SeedDataCreate(review, user);
        }
        public void UpdateBookRating(ReviewInputModel review)
        {
            _bookRepo.UpdateBookRating(review);
        }
        public BookListViewModel GetBookByReview(ReviewInputModel review)
        {
            return _bookRepo.GetBookByReview(review);
        }
        public List<BookListViewModel> GetBooksByGenre(string genre)
        {
            return _bookRepo.GetBooksByGenre(genre);
        }
        public List<BookListViewModel> GetBooksOnSale()
        {
            return _bookRepo.GetBooksOnSale();
        }
        public List<BookListViewModel> GetBooksOnSaleAZ()
        {
            return _bookRepo.GetBooksOnSaleAZ();
        }
        public List<BookListViewModel> GetBooksOnSaleLH()
        {
            return _bookRepo.GetBooksOnSaleLH();
        }
        public List<BookListViewModel> GetBooksOnSaleHL()
        {
            return _bookRepo.GetBooksOnSaleHL();
        }
        public List<BookListViewModel> GetBooksOnSaleNewest()
        {
            return _bookRepo.GetBooksOnSaleNewest();
        }
        public List<BookListViewModel> GetBooksByAuthorAZ()
        {
            return _bookRepo.GetBooksByAuthorAZ();
        }
        public List<BookListViewModel> GetBooksGenreOrderAZ()
        {
            return _bookRepo.GetBooksGenreOrderAZ();
        }
        
        public void ProcessBook(BookInputModel book)
        {
            if(string.IsNullOrEmpty(book.Title))
            {
                throw new Exception("Title is missing");
            }
            if(string.IsNullOrEmpty(book.Author))
            {
                throw new Exception("Author is missing");
            }
            if(book.Price == 0)
            {
                throw new Exception("Price is missing");
            }
            if(string.IsNullOrEmpty(book.Image))
            {
                throw new Exception("Image is missing");
            }
            if(string.IsNullOrEmpty(book.Genre))
            {
                throw new Exception("Genre is missing");
            }
            if(book.Quantity == 0)
            {
                throw new Exception("Quantity is missing");
            }
            if(string.IsNullOrEmpty(book.Description))
            {
                throw new Exception("Description is missing");
            }
        }
    }
}