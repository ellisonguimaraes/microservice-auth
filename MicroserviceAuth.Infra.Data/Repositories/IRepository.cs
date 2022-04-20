using FluentResults;
using MicroserviceAuth.Domain.Pagination;

namespace MicroserviceAuth.Infra.Data.Repositories;

public interface IRepository<TEntity>
{
    public Result<PagedList<TEntity>> GetPaginate(PaginationParameters paginationParameters);

    public Task<Result<TEntity>> GetByIdAsync(Guid id);

    public Task<Result<TEntity>> CreateAsync(TEntity entity);

    public Task<Result<TEntity>> UpdateAsync(TEntity entity);

    public Task<Result<TEntity>> DeleteAsync(Guid id);
}
