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