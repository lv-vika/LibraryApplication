using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;

namespace LibraryApplication.Infrastructure.Repositories;

public class AuthorRepository : BaseCrudRepository<AuthorEntity>, IAuthorRepository
{
    public AuthorRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(AuthorEntity entityToUpdate, AuthorEntity passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
        entityToUpdate.Surname = passedEntity.Surname;
    }
}