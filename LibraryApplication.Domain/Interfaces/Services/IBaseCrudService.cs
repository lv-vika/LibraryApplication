using LibraryApplication.Data.Models;

namespace LibraryApplication.Data.Interfaces.Services;

public interface IBaseCrudService<TModel> 
    where TModel:IModelBase 
{
    Task<List<TModel>> GetAll();

    Task<int> Create(TModel entity);

    Task<TModel> GetById(int id);

    Task<bool> Update(int id, TModel entity);

    Task<bool> Delete(int id);
}