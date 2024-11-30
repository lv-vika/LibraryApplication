using LibraryApplication.Data.Database.Entities;

namespace LibraryApplication.Data.Interfaces.Repositories;

public interface IDiscountRepository : IBaseCrudRepository<DiscountEntity>
{
    public Task<DiscountEntity> GetByName(string name);
}