using LibraryApplication.Data.Database.Base;

namespace LibraryApplication.Data.Interfaces.Repositories;

public interface IBaseCrudRepository<T> where T:IEntityBase
{
    Task<List<T>> GetAll();

    Task<int> Create(T entity);

    Task<T> GetById(int id);

    Task<bool> Update(int id, T entity);

    Task<bool> Delete(int id);
}