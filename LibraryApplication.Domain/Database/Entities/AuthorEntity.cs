using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class AuthorEntity : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public virtual List<BookEntity> Books { get; set; }
}