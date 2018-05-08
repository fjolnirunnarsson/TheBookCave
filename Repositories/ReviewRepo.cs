using TheBookCave.Data;
using System.Collections.Generic;
using System.Linq;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class ReviewRepo
    {
        private DataContext _db;

        public ReviewRepo(){
            _db = new DataContext();
        }

        public List<ReviewViewModel> GetAllReviews()
        {

            var reviews = (from a in _db.Reviews
                            select new ReviewViewModel
                            {
                                Id = a.Id,
                                Rating = a.Rating,
                                Comment = a.Comment,
                                UserName = a.UserName,
                                BookId = a.BookId
                            }).ToList();

            return reviews;
        }
    }
}