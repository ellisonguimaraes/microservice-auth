using FluentResults;
using MicroserviceAuth.Domain.DTO.Application;
using MicroserviceAuth.Domain.Pagination;

namespace MicroserviceAuth.Service.Application;

public interface IApplicationServices
{
    public Result<PagedList<ApplicationResponse>> GetPaginate(PaginationParameters paginationParameters);
    public Task<Result<ApplicationResponse>> GetByIdAsync(Guid id);
    public Task<Result<ApplicationResponse>> CreateAsync(string applicationName);
    Task<Result<ApplicationResponse>> UpdateAsync(ApplicationRequest applicationRequest);
    public Task<Result<ApplicationResponse>> DeleteAsync(Guid id);
}
