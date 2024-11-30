using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure.Repositories;

public class UserRepository : BaseCrudRepository<UserEntity>, IUserRepository
{
    public UserRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(UserEntity entityToUpdate, UserEntity passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
        entityToUpdate.Surname = passedEntity.Surname;
        entityToUpdate.Address = passedEntity.Address;
        entityToUpdate.Login = passedEntity.Login;
        entityToUpdate.Password = passedEntity.Password;
        entityToUpdate.UserCategories = passedEntity.UserCategories;
        entityToUpdate.Balance = passedEntity.Balance;
    }

    public async Task<double?> UpdateUserBalance(int id, double amountToAdd)
    {
        var user = await this.GetById(id);

        if (user is null || user.Balance + amountToAdd < 0)
        {
            return null;
        }

        user.Balance += amountToAdd;

        this.DbContext.Update(user);
        await this.DbContext.SaveChangesAsync();

        return user.Balance;
    }

    public Task<List<UserBalanceTransferEntity>> GetUserBalanceHistory(int id)
    {
        return this.DbContext.UserBalanceTransfers.Where(x => x.UserId == id).ToListAsync();
    }

    public async Task<int?> GetUserIdByLoginAndPassword(string login, string password)
    {
        var user = await this.DbContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

        return user?.Id;
    }

    public bool CheckIfUserExists(int id)
    {
        return this.DbContext.Users.Any(x => x.Id == id);
    }

    public Task<List<BookTransferEntity>> GetUserValidTransfers(int userId)
    {
        var allTransfers= this.DbContext.BookTransfers
            .Where(x => x.UserId == userId);
        
        return allTransfers
            .ToListAsync();
    }
}