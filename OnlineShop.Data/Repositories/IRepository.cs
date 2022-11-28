namespace OnlineShop.Data.Repositories;

public interface IRepository<TEntity>
{
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
}