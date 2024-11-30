namespace LibraryApplication.Data.Models;

public class FineModel : IModelBase
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    
    public int Amount { get; set; }
}