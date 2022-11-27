namespace OnlineShop.WebApi.Data;

public interface IRepository<TEntity>
{
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken);
    Task Add(TEntity entity, CancellationToken cancellationToken);
    Task Update(TEntity entity, CancellationToken cancellationToken);
    Task Delete(TEntity entity, CancellationToken cancellationToken);
}