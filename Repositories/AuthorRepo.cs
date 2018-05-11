using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class AuthorRepo
    {
        private DataContext _db;
        public AuthorRepo()
        {
            _db = new DataContext();
        }
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = (from a in _db.Authors
                           select new AuthorListViewModel
                           {
                               Id = a.Id,
                               Name = a.Name
                           }).ToList();

            return authors;
        }
    }
}