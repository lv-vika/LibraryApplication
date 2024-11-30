namespace LibraryApplication.Data.Models;

public class BookModel : IModelBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsAvailable { get; set; }

    public int AuthorId { get; set; }

    public double RentPrice { get; set; }

    public int GenreId { get; set; }
}