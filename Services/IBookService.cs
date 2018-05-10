using System;
using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class IBookService
    {
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