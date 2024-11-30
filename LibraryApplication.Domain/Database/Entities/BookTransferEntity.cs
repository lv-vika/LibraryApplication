using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class BookTransferEntity : IEntityBase
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public bool IsBorrowed { get; set; }

    public bool IsReturned { get; set; }

    public DateTime TransferDate { get; set; }

    public DateTime? ExpectedReturnDate { get; set; }

    public virtual BookEntity BookEntity { get; set; }

    public virtual UserEntity UserEntity { get; set; }

    public virtual List<FineEntity> Fines { get; set; }
}