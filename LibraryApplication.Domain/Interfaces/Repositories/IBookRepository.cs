using LibraryApplication.Data.Database.Entities;

namespace LibraryApplication.Data.Interfaces.Repositories;

public interface IBookRepository : IBaseCrudRepository<BookEntity>
{
    Task<bool> MarkBookAsBorrowed(int id);
    
    Task<bool> MarkBookAsAvailable(int id);

    Task<int> CreateBookTransfer(int bookId, int userId, bool isBorrowed, int? rentTimeInDays);

    Task<List<BookEntity>> GetAllBorrowedBooks();
    
    bool CheckIfBookExists(int id);
    
    Task<List<BookEntity>> GetAllAvailableBooks();
    
    Task<List<BookEntity>> GetBorrowedBooksByUser(int userId);
}