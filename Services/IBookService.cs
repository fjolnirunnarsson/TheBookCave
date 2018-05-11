using TheBookCave.Models.InputModels;

namespace TheBookCave.Services
{
    public interface IBookService
    {
        void ProcessBook(BookInputModel book);
    }
}