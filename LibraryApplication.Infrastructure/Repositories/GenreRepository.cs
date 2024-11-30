using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;

namespace LibraryApplication.Infrastructure.Repositories;

public class GenreRepository : BaseCrudRepository<BookGenre>, IGenreRepository
{
    public GenreRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(BookGenre entityToUpdate, BookGenre passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
    }
}