using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class GenreService : BaseCrudService<BookGenreModel, BookGenre>, IGenreService
{
    public GenreService(IGenreRepository genreRepository, IMapper mapper) 
        : base(genreRepository, mapper)
    {
    }
}