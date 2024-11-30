using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;

namespace LibraryApplication.Infrastructure.Repositories;

public class TransferTypeRepository : BaseCrudRepository<TransferType>, ITransferTypeRepository
{
    public TransferTypeRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(TransferType entityToUpdate, TransferType passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
    }
}