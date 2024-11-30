using LibraryApplication.Data.Models;

namespace LibraryApplication.Data.Interfaces.Services;

public interface IUserService : IBaseCrudService<UserModel>
{
    Task<int?> Authenticate(string userInput, string passwordInput);
    Task<bool> TryProcessFinePayment(int userId, int bookId);
    Task<bool> HasFines(int userId);
    Task<List<BookTransferModel>> GetUserValidTransfers(int userId);
}