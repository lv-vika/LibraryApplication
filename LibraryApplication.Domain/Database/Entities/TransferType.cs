using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class TransferType : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual List<UserBalanceTransferEntity> UserBalanceTransfers { get; set; }

    public virtual List<BudgetTransferEntity> BudgetTransfers { get; set; }
}