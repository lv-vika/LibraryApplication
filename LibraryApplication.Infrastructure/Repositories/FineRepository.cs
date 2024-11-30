using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure.Repositories;

public class FineRepository : BaseCrudRepository<FineEntity>, IFineRepository
{
    public FineRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(FineEntity entityToUpdate, FineEntity passedEntity)
    {
        entityToUpdate.Amount = passedEntity.Amount;
        entityToUpdate.UserId = passedEntity.UserId;
        entityToUpdate.BookTransferId = passedEntity.BookTransferId;
    }

    public Task<List<FineEntity>> GetFinesByUserId(int userId)
    {
        return this.DbContext.Fines.Where(x => x.UserId == userId).ToListAsync();
    }
}