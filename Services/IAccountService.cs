using TheBookCave.Models.InputModels;

namespace TheBookCave.Services
{
    public interface IAccountService
    {
        void ProcessAccountCheckout(AccountInputModel account);
    }
}