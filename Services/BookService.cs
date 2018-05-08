using System.Collections.Generic;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class BookService
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
        
    }
}