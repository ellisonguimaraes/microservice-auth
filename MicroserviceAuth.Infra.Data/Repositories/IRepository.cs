using FluentResults;

namespace MicroserviceAuth.Infra.Data.Repositories;

public interface IRepository<TEntity>
{
    public Task<List<TEntity>> GetAllAsync();

    public Task<Result<TEntity>> GetByIdAsync(Guid id);

    public Task<Result<TEntity>> CreateAsync(TEntity entity);

    public Task<Result<TEntity>> UpdateAsync(TEntity entity);

    public Task<Result<TEntity>> DeleteAsync(Guid id);
}
