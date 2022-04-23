using FluentResults;
using MicroserviceAuth.Domain.Application;

namespace MicroserviceAuth.Infra.Data.Repositories;

public interface IApplicationRepository : IRepository<Application>
{
    public Task<Result<Application>> GetByApplicationNameAsync(string applicationName);
    public Task<Result<Application>> GetByApiKeyAsync(string apiKey);
}