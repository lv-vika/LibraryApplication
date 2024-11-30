using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;

namespace LibraryApplication.Service.Services;

public class AdminService : IAdminService
{
    private readonly IFineRepository fineRepository;
    private readonly IBookRepository bookRepository;
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public AdminService(
        IFineRepository fineRepository,
        IBookRepository bookRepository,
        IDiscountRepository discountRepository, 
        IMapper mapper)
    {
        this.fineRepository = fineRepository;
        this.bookRepository = bookRepository;
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task GenerateFinesPastDueDate(int amount)
    {
        foreach (var book in await bookRepository.GetAllBorrowedBooks())
        {
            var bookTransferEntity = book.BookTransfers.LastOrDefault();
            if (bookTransferEntity is not null && bookTransferEntity.IsBorrowed && bookTransferEntity.ExpectedReturnDate <= DateTime.Now)
            {
                var finesByUserId = await fineRepository.GetFinesByUserId(bookTransferEntity.UserEntity.Id);
                await CreateOrUpdateFine(amount, finesByUserId, bookTransferEntity);
            }
        }
    }

    public async Task<bool> GenerateFineForBookDamage(int bookId, int amount)
    {
        BookEntity bookEntity = await bookRepository.GetById(bookId);

        if (bookEntity is null)
        {
            return false;
        }

        var bookTransferEntity = bookEntity.BookTransfers.LastOrDefault();

        if (bookTransferEntity is null)
        {
            return false;
        }

        var finesByUserId = await fineRepository.GetFinesByUserId(bookTransferEntity.UserId);
        await CreateOrUpdateFine(amount, finesByUserId, bookTransferEntity);
        return true;
    }

    private async Task CreateOrUpdateFine(int amount, IEnumerable<FineEntity> finesByUserId, BookTransferEntity bookTransferEntity)
    {
        var existingFine = finesByUserId.FirstOrDefault(x => x.BookTransferId == bookTransferEntity.Id);

        if (existingFine is null)
        {
            await fineRepository.Create(new FineEntity
            {
                BookTransferId = bookTransferEntity.Id,
                Date = DateTime.Now,
                UserId = bookTransferEntity.UserId,
                Amount = amount,
            });
        }
        else
        {
            existingFine.Amount += amount;
            await fineRepository.Update(existingFine.Id, existingFine);
        }
    }
}