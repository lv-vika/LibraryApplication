using LibraryApplication.Data.Database.Entities;

namespace LibraryApplication.Data.Interfaces.Repositories;

public interface IUserRepository : IBaseCrudRepository<UserEntity>
{
    Task<double?> UpdateUserBalance(int id, double amountToAdd);

    Task<List<UserBalanceTransferEntity>> GetUserBalanceHistory(int id);

    Task<int?> GetUserIdByLoginAndPassword(string login, string password);

    bool CheckIfUserExists(int id);
    
    Task<List<BookTransferEntity>> GetUserValidTransfers(int userId);
}