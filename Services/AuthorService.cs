using System.Collections.Generic;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class AuthorService
    {
        private AuthorRepo _authorRepo;

        public AuthorService()
        {
            _authorRepo = new AuthorRepo();
        }

        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = _authorRepo.GetAllAuthors();

            return authors;
        }
    }
}