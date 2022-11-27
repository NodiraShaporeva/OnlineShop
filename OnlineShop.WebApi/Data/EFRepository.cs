using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.WebApi.Data;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

    public virtual Task<TEntity> GetById(Guid id, CancellationToken cancellationToken)
        => Entities.FirstAsync(it => it.Id == id, cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken)
        => await Entities.ToListAsync(cancellationToken);

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Delete(TEntity entity, CancellationToken cancellationToken)
    {
        Entities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}