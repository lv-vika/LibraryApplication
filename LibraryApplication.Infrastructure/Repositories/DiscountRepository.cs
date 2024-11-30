using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure.Repositories;

public class DiscountRepository : BaseCrudRepository<DiscountEntity>, IDiscountRepository
{
    public DiscountRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(DiscountEntity entityToUpdate, DiscountEntity passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
        entityToUpdate.Amount = passedEntity.Amount;
    }

    public Task<DiscountEntity> GetByName(string name)
    {
        return this.DbContext.Discounts.FirstOrDefaultAsync(x => x.Name == name);
    }
}