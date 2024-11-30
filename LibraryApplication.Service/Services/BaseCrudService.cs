using AutoMapper;
using LibraryApplication.Data.Database.Base;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public abstract class BaseCrudService<TModel, TEntity> : IBaseCrudService<TModel> 
    where TModel:IModelBase 
    where TEntity:IEntityBase
{
    protected readonly IBaseCrudRepository<TEntity> BaseCrudRepository;
    protected readonly IMapper Mapper;

    protected BaseCrudService(IBaseCrudRepository<TEntity> baseCrudRepository,IMapper mapper)
    {
        this.Mapper = mapper;
        this.BaseCrudRepository = baseCrudRepository;
    }

    public async Task<List<TModel>> GetAll()
    {
        var entities = await this.BaseCrudRepository.GetAll();
        return this.Mapper.Map<List<TModel>>(entities);
    }

    public Task<int> Create(TModel model)
    {
        var entity = this.Mapper.Map<TEntity>(model);
        return this.BaseCrudRepository.Create(entity);
    }

    public async Task<TModel> GetById(int id)
    {
        var entity = await this.BaseCrudRepository.GetById(id);
        return this.Mapper.Map<TModel>(entity);
    }

    public Task<bool> Update(int id, TModel model)
    {
        var entity = this.Mapper.Map<TEntity>(model);
        return this.BaseCrudRepository.Update(id, entity);
    }

    public Task<bool> Delete(int id)
    {
        return this.BaseCrudRepository.Delete(id);
    }
}