using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class BookEntity : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsAvailable { get; set; }

    public int AuthorId { get; set; }

    public double RentPrice { get; set; }

    public int GenreId { get; set; }

    public virtual AuthorEntity AuthorEntity { get; set; }

    public virtual BookGenre Genre { get; set; }

    public virtual List<BookTransferEntity> BookTransfers { get; set; }
}