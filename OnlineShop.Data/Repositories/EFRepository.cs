using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;

    protected EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public virtual Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
        => Entities.FirstAsync(it => it.Id == id, cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
       var products = await Entities.ToListAsync(cancellationToken);
       return products;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Entities.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Entities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}