using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure.Repositories;

public class BookRepository : BaseCrudRepository<BookEntity>, IBookRepository
{
    public BookRepository(LibraryApplicationDbContext dbContext) : base(dbContext)
    {
    }

    protected override void UpdateProps(BookEntity entityToUpdate, BookEntity passedEntity)
    {
        entityToUpdate.Name = passedEntity.Name;
        entityToUpdate.GenreId = passedEntity.GenreId;
        entityToUpdate.AuthorId = passedEntity.AuthorId;
        entityToUpdate.RentPrice = passedEntity.RentPrice;
        entityToUpdate.IsAvailable = passedEntity.IsAvailable;
    }

    public override Task<BookEntity> GetById(int id)
    {
        return this.DbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<bool> MarkBookAsBorrowed(int id)
    {
        return this.ChangeBookAvailabilityStatus(id, false);
    }

    public Task<bool> MarkBookAsAvailable(int id)
    {
        return this.ChangeBookAvailabilityStatus(id, true);
    }

    public async Task<int> CreateBookTransfer(int bookId, int userId, bool isBorrowed, int? rentTimeInDays)
    {
        BookTransferEntity bookTransfer = new()
        {
            TransferDate = DateTime.Now,
            BookId = bookId,
            IsBorrowed = isBorrowed,
            UserId = userId,
            IsReturned = !isBorrowed,
            ExpectedReturnDate = rentTimeInDays is not null ? DateTime.Now.AddDays((double)rentTimeInDays) : null
        };

        var added = await this.DbContext.BookTransfers.AddAsync(bookTransfer);
        await this.DbContext.SaveChangesAsync();

        return added.Entity.Id;
    }

    public Task<List<BookEntity>> GetAllBorrowedBooks()
    {
        return this.DbContext.Books.Where(x => !x.IsAvailable).ToListAsync();
    }

    public Task<List<BookEntity>> GetAllAvailableBooks()
    {
        return this.DbContext.Books.Where(x => x.IsAvailable).ToListAsync();
    }

    public Task<List<BookEntity>> GetBorrowedBooksByUser(int userId)
    {
        return this.DbContext.Books
            .Where(x => !x.IsAvailable 
                        && x.BookTransfers.OrderBy(x => x.TransferDate)
                            .LastOrDefault().UserId == userId)
            .ToListAsync();
    }

    public bool CheckIfBookExists(int id)
    {
        return this.DbContext.Books.Any(x => x.Id == id);
    }

    private async Task<bool> ChangeBookAvailabilityStatus(int bookId, bool isAvailable)
    {
        var book = await GetById(bookId);
        book.IsAvailable = isAvailable;

        var updated = this.DbContext.Update(book);
        await this.DbContext.SaveChangesAsync();

        return updated.Entity.IsAvailable == isAvailable;
    }
}