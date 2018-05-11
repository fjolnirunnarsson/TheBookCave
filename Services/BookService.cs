using System;
using System.Collections.Generic;
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
        
        public BookListViewModel GetBookByTitle(string title)
        {
            return _bookRepo.GetBookByTitle(title);
        }
        
        public BookListViewModel GetBookByReview(ReviewInputModel review)
        {
            return _bookRepo.GetBookByReview(review);
        }
        
        public BookListViewModel GetBookById(int? id)
        {
            return _bookRepo.GetBookById(id);
        }
        
        public List<BookListViewModel> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
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
        
        public List<BookListViewModel> GetBooksBoughtOrder()
        {
            return _bookRepo.GetBooksBoughtOrder();
        }
        
        public List<BookListViewModel> GetBooksByDiscount()
        {
            return _bookRepo.GetBooksByDiscount();
        }
        
        public List<BookListViewModel> GetBooksByQuantity()
        {
            return _bookRepo.GetBooksByQuantity();
        }
        
        public List<BookListViewModel> GetBooksOrderSold()
        {
            return _bookRepo.GetBooksOrderSold();
        }

        public List<BookListViewModel> GetShoppingCartBooks(string cartId)
        {
            return _bookRepo.GetShoppingCartBooks(cartId);
        }
        
        public List<ReviewViewModel> GetAllReviews()
        {

            var reviews = _reviewRepo.GetAllReviews();

            return reviews;
        }
        
        public List<ReviewViewModel> GetBookReviews(string title)
        {
            return _bookRepo.GetBookReviews(title);
        }
        
        public void UpdateBookRating(ReviewInputModel review)
        {
            _bookRepo.UpdateBookRating(review);
        }
        
        public void ProcessBook(BookInputModel book)
        {
            if (string.IsNullOrEmpty(book.Title))
            {
                throw new Exception("Title is missing");
            }
            if (string.IsNullOrEmpty(book.Author))
            {
                throw new Exception("Author is missing");
            }
            if (book.Price == 0)
            {
                throw new Exception("Price is missing");
            }
            if (string.IsNullOrEmpty(book.Image))
            {
                throw new Exception("Image is missing");
            }
            if (string.IsNullOrEmpty(book.Genre))
            {
                throw new Exception("Genre is missing");
            }
            if (book.Quantity == 0)
            {
                throw new Exception("Quantity is missing");
            }
            if (string.IsNullOrEmpty(book.Description))
            {
                throw new Exception("Description is missing");
            }
        }
        
        public void SeedDataCreateReview(ReviewInputModel review, string user)
        {
            _bookRepo.SeedDataCreateReview(review, user);
        }
        public void SeedDataCreateAccount(RegisterViewModel model)
        {
            _bookRepo.SeedDataCreateAccount(model);
        }
        
        public void SeedDataCreateBook(BookInputModel model)
        {
            _bookRepo.SeedDataCreateBook(model);
        }
        
        public void SeedDataChangeBook(BookInputModel updatedBook)
        {
            _bookRepo.SeedDataChangeBook(updatedBook);
        }
        
        public void SeedDataDeleteBook(BookInputModel deleteBook)
        {
            _bookRepo.SeedDataDeleteBook(deleteBook);
        }  
    }
}