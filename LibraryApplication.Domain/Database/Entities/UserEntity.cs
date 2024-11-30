using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class UserEntity : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Address { get; set; }

    public bool IsAdmin { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public double Balance { get; set; }

    public DateTime RegisterDate { get; set; }

    public virtual List<UserCategoryEntity> UserCategories { get; set; } = new();

    public virtual List<FineEntity> Fines { get; set; }

    public virtual List<UserBalanceTransferEntity> UserBalanceTransfers { get; set; }
}