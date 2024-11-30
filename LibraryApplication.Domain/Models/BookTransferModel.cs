namespace LibraryApplication.Data.Models;

public class BookTransferModel : IModelBase
{
    public int Id { get; set; }
    
    public int BookId { get; set; }
    
    public int UserId { get; set; }

    public string BookName { get; set; }

    public string Genre { get; set; }
    
    public string Author { get; set; }

    public double RentPrice { get; set; }

    public DateTime TransferDate { get; set; }

    public DateTime? ExpectedReturnDate { get; set; }

    public bool HasFines { get; set; }
}