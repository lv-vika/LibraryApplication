namespace LibraryApplication.Data.Models;

public class UserModel : IModelBase
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Address { get; set; }

    public bool IsAdmin { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public double Balance { get; set; }

    public DateTime RegisterDate { get; set; }
}