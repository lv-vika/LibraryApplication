using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class AuthorService : BaseCrudService<AuthorModel, AuthorEntity>, IAuthorService
{
    public AuthorService(IAuthorRepository authorRepository, IMapper mapper) 
        : base(authorRepository, mapper)
    {
    }
}