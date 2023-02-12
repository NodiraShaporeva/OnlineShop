﻿using OnlineShop.Domain;
using OnlineShop.Domain.RepositoriesInterfaces;

namespace OnlineShop.Data;

public class UnitOfWork: IUnitOfWork, IAsyncDisposable
{
    public IAccountRepository AccountRepository { get; }
    public ICartRepository CartRepository { get; }
    private readonly AppDbContext _dbContext;

    public UnitOfWork(
            AppDbContext dbContext,
            IAccountRepository accountRepository,
            ICartRepository cartRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}