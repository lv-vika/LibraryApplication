namespace LibraryApplication.Data.Models;

public class AuthorModel : IModelBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
}