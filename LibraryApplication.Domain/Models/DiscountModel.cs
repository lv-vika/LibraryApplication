namespace LibraryApplication.Data.Models;

public class DiscountModel : IModelBase
{
    public int Id { get; set; }
    
    public int Amount { get; set; }
    
    public string Name { get; set; }
}