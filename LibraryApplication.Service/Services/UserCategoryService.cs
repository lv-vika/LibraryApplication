using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class UserCategoryService : BaseCrudService<UserCategoryModel, UserCategoryEntity>, IUserCategoryService
{
    public UserCategoryService(IUserCategoryRepository userCategoryRepository, IMapper mapper) 
        : base(userCategoryRepository, mapper)
    {
    }
}