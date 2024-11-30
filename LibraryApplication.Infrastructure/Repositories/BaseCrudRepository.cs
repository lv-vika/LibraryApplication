using LibraryApplication.Data.Database.Base;
using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure.Repositories;

public abstract class BaseCrudRepository<T> : IBaseCrudRepository<T> 
    where T: class, IEntityBase
{
    protected readonly LibraryApplicationDbContext DbContext;

    protected BaseCrudRepository(LibraryApplicationDbContext dbContext)
    {
        this.DbContext = dbContext;
    }
    
    public virtual Task<List<T>> GetAll()
    {
        return this.DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<int> Create(T entity)
    {
        var added = await this.DbContext.Set<T>().AddAsync(entity);
        await this.DbContext.SaveChangesAsync();

        return added.Entity.Id;
    }

    public virtual Task<T> GetById(int id)
    {
        return this.DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<bool> Update(int id, T entity)
    {
        var entityToUpdate = await GetById(id);

        if (entityToUpdate is null)
        {
            return false;
        }

        UpdateProps(entityToUpdate, entity);

        var updated = this.DbContext.Set<T>().Update(entityToUpdate);
        await this.DbContext.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> Delete(int id)
    {
        var entityToDelete = await GetById(id);

        if (entityToDelete is null)
        {
            return false;
        }

        var deleted = this.DbContext.Remove(entityToDelete);
        await this.DbContext.SaveChangesAsync();

        return true;
    }

    protected abstract void UpdateProps(T entityToUpdate, T passedEntity);
}