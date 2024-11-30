using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;

namespace LibraryApplication.Infrastructure.Repositories;

public class UserCategoryRepository : BaseCrudRepository<UserCategoryEntity>, IUserCategoryRepository
{
    public UserCategoryRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(UserCategoryEntity entityToUpdate, UserCategoryEntity passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
    }
}