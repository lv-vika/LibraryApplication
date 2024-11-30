using LibraryApplication.Data.Models;

namespace LibraryApplication.Data.Interfaces.Services;

public interface IDiscountService : IBaseCrudService<DiscountModel>
{
    public Task<DiscountModel> GetByName(string name);
}