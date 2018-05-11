using TheBookCave.Models.InputModels;

namespace TheBookCave.Services
{
    public interface IBookService
    {
        void ProcessBookCheckout(BookInputModel book);
    }
}