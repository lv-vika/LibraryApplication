using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class BookGenre : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual List<BookEntity> Books { get; set; }
}   