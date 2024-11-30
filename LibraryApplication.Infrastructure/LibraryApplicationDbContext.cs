using LibraryApplication.Data.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Infrastructure;

public sealed class LibraryApplicationDbContext: DbContext
{
    public LibraryApplicationDbContext(DbContextOptions<LibraryApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
    
    public DbSet<BookEntity> Books => Set<BookEntity>();
    
    public DbSet<BookGenre> BookGenres => Set<BookGenre>();
    
    public DbSet<BookTransferEntity> BookTransfers => Set<BookTransferEntity>();
    
    public DbSet<BudgetTransferEntity> BudgetTransfers => Set<BudgetTransferEntity>();
    
    public DbSet<DiscountEntity> Discounts => Set<DiscountEntity>();
    
    public DbSet<FineEntity> Fines => Set<FineEntity>();
    
    public DbSet<TransferType> TransferTypes => Set<TransferType>();

    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<UserBalanceTransferEntity> UserBalanceTransfers => Set<UserBalanceTransferEntity>();

    public DbSet<UserCategoryEntity> UserCategories => Set<UserCategoryEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCategoryEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserCategoryEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        
        modelBuilder.Entity<BookEntity>()
            .HasOne(x => x.AuthorEntity)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId);
        
        modelBuilder.Entity<BookEntity>()
            .HasOne(x => x.Genre)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.GenreId);
        
        modelBuilder.Entity<BookTransferEntity>()
            .HasOne(x => x.BookEntity)
            .WithMany(x => x.BookTransfers)
            .HasForeignKey(x => x.BookId);

        modelBuilder.Entity<FineEntity>()
            .HasOne(x => x.UserEntity)
            .WithMany(x => x.Fines)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<FineEntity>()
            .HasOne(x => x.BookTransferEntity)
            .WithMany(x => x.Fines)
            .HasForeignKey(x => x.BookTransferId);

        modelBuilder.Entity<UserEntity>()
            .HasMany(x => x.UserCategories)
            .WithMany(x => x.Users);
        
        modelBuilder.Entity<UserBalanceTransferEntity>()
            .HasOne(x => x.UserEntity)
            .WithMany(x => x.UserBalanceTransfers)
            .HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<UserBalanceTransferEntity>()
            .HasOne(x => x.TransferType)
            .WithMany(x => x.UserBalanceTransfers)
            .HasForeignKey(x => x.TransferTypeId);
        
        modelBuilder.Entity<BudgetTransferEntity>()
            .HasOne(x => x.TransferType)
            .WithMany(x => x.BudgetTransfers)
            .HasForeignKey(x => x.TransferTypeId);
    }
}