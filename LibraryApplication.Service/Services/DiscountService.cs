using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class DiscountService : BaseCrudService<DiscountModel, DiscountEntity>, IDiscountService
{
    private readonly IDiscountRepository discountRepository;
    
    public DiscountService(IDiscountRepository discountRepository, IMapper mapper) 
        : base(discountRepository, mapper)
    {
        this.discountRepository = discountRepository;
    }

    public async Task<DiscountModel> GetByName(string name)
    {
        var discount = await this.discountRepository.GetByName(name);
        return this.Mapper.Map<DiscountModel>(discount);
    }
}