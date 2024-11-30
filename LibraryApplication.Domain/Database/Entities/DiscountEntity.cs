using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class DiscountEntity : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Amount { get; set; }
    

    public virtual List<BookTransferEntity> BookTransfers { get; set; }
    
}