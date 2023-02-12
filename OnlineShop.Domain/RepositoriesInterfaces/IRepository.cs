using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.RepositoriesInterfaces;

public interface IRepository<TEntity> where TEntity: IEntity
{
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
}