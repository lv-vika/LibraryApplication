using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class FineEntity : IEntityBase
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookTransferId { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }

    public virtual UserEntity UserEntity { get; set; }

    public virtual BookTransferEntity BookTransferEntity { get; set; }
}