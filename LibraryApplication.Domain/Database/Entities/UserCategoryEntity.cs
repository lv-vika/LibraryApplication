using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Database.Entities;

public class UserCategoryEntity : IEntityBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual List<UserEntity> Users { get; set; } = new();
    
}