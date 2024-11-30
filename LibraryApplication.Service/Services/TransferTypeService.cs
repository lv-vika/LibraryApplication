using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class TransferTypeService : BaseCrudService<TransferTypeModel, TransferType>, ITransferTypeService
{
    public TransferTypeService(ITransferTypeRepository transferTypeRepository, IMapper mapper) 
        : base(transferTypeRepository, mapper)
    {
    }
}