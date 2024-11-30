using AutoMapper;
using LibraryApplication.Data.Database.Entities;
using LibraryApplication.Data.Interfaces.Repositories;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;

namespace LibraryApplication.Service.Services;

public class UserService : BaseCrudService<UserModel, UserEntity>, IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IFineRepository fineRepository;
    private readonly IBookRepository bookRepository;

    public UserService(
        IUserRepository userRepository,
        IFineRepository fineRepository, 
        IMapper mapper, IBookRepository bookRepository) 
        : base(userRepository, mapper)
    {
        this.userRepository = userRepository;
        this.fineRepository = fineRepository;
        this.bookRepository = bookRepository;
    }

    public Task<int?> Authenticate(string userInput, string passwordInput)
    {
        return userRepository.GetUserIdByLoginAndPassword(userInput, passwordInput);
    }

    public async Task<bool> HasFines(int userId)
    {
        return (await fineRepository.GetFinesByUserId(userId)).Any();
    }

    public async Task<List<BookTransferModel>> GetUserValidTransfers(int userId)
    {
        var transfers = await this.userRepository.GetUserValidTransfers(userId);
        var books = await this.bookRepository.GetBorrowedBooksByUser(userId);
        var result = transfers.Where(x => 
            books.Select(x => x.Id).Contains(x.BookId) && x.IsBorrowed).GroupBy(x => x.BookId)
            .Select(x =>x.LastOrDefault());
        return this.Mapper.Map<List<BookTransferModel>>(result);
    }

    public async Task<bool> TryProcessFinePayment(int userId, int bookId)
    {
        var fineEntity = (await fineRepository.GetFinesByUserId(userId)).FirstOrDefault(x => x.BookTransferEntity.BookId == bookId);

        if (fineEntity is null)
        {
            return false;
        }

        var updateResult = await userRepository.UpdateUserBalance(userId, -fineEntity.Amount);
        var deleteResult = await fineRepository.Delete(fineEntity.Id);
        return updateResult is not null || deleteResult;
    }
}