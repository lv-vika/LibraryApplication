namespace LibraryApplication.Data.Models;

public class BorrowBookModel
{
    public int UserId { get; set; }
    
    public int RentInDays { get; set; }

    public int? DiscountId { get; set; }
}