using System;
using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public interface IBookService
    {
        void ProcessBook(BookInputModel book);
    }
}