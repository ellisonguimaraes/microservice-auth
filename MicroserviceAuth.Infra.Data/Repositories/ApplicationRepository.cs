using FluentResults;
using MicroserviceAuth.Domain.Application;
using MicroserviceAuth.Domain.Pagination;
using MicroserviceAuth.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MicroserviceAuth.Infra.Data.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    private readonly ILogger<ApplicationRepository> _logger;

    public ApplicationRepository(ApplicationDbContext applicationDbContext, ILogger<ApplicationRepository> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public Result<PagedList<Application>> GetPaginate(PaginationParameters paginationParameters)
    {
        try
        {
            var applications = _applicationDbContext.Applications
            .Include(a => a.ApplicationUsers).ThenInclude(au => au.User)
            .OrderBy(a => a.Id);

            return Result.Ok(new PagedList<Application>(applications, paginationParameters.PageSize, paginationParameters.PageNumber));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting applications");
            return Result.Fail("Error getting applications");
        }
    }

    public async Task<Result<Application>> GetByIdAsync(Guid id)
    {
        var entity = await _applicationDbContext.Applications
            .Include(a => a.ApplicationUsers).ThenInclude(au => au.User)
            .Where(a => a.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return Result.Fail("Application not found");
        }

        return Result.Ok(entity);
    }

    public async Task<Result<Application>> CreateAsync(Application entity)
    {
        try
        {
            await _applicationDbContext.Applications.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, JsonSerializer.Serialize(entity));
            return Result.Fail("Error creating application");
        }
    }

    public async Task<Result<Application>> UpdateAsync(Application entity)
    {
        var getEntity = await _applicationDbContext.Applications.Where(a => a.Id.Equals(entity.Id)).SingleOrDefaultAsync();

        if (getEntity == null)
        {
            return Result.Fail("Application not found");
        }

        try
        {
            getEntity.ApplicationName = entity.ApplicationName ?? getEntity.ApplicationName;
            getEntity.ApiKey = entity.ApiKey ?? getEntity.ApiKey;
            getEntity.EditedAt = DateTime.UtcNow;
            getEntity.EditedBy = entity.EditedBy ?? getEntity.EditedBy;

            _applicationDbContext.Applications.Update(getEntity);
            await _applicationDbContext.SaveChangesAsync();

            return Result.Ok(getEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, JsonSerializer.Serialize(entity));
            return Result.Fail("Error updating application");
        }
    }

    public async Task<Result<Application>> DeleteAsync(Guid id)
    {
        var getEntity = await _applicationDbContext.Applications.Where(a => a.Id.Equals(id)).SingleOrDefaultAsync();

        if (getEntity == null)
        {
            return Result.Fail("Application not found");
        }

        try
        {
            _applicationDbContext.Applications.Remove(getEntity);
            await _applicationDbContext.SaveChangesAsync();

            return Result.Ok(getEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, JsonSerializer.Serialize(getEntity));
            return Result.Fail("Error removing application");
        }
    }

    public async Task<Result<Application>> GetByApplicationNameAsync(string applicationName)
    {
        var entity = await _applicationDbContext.Applications
            .Include(a => a.ApplicationUsers).ThenInclude(au => au.User)
            .Where(a => a.ApplicationName.Equals(applicationName))
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return Result.Fail("Application not found");
        }

        return Result.Ok(entity);
    }

    public async Task<Result<Application>> GetByApiKeyAsync(string apiKey)
    {
        var entity = await _applicationDbContext.Applications
            .Include(a => a.ApplicationUsers).ThenInclude(au => au.User)
            .Where(a => a.ApiKey.Equals(apiKey))
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return Result.Fail("Application not found");
        }

        return Result.Ok(entity);
    }
}
