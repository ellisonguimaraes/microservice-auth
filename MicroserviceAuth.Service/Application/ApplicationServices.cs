using AutoMapper;
using FluentResults;
using MicroserviceAuth.Domain.DTO.Application;
using MicroserviceAuth.Domain.Pagination;
using MicroserviceAuth.Infra.Data.Repositories;

namespace MicroserviceAuth.Service.Application;

public class ApplicationServices : IApplicationServices
{
    private readonly IApplicationRepository _applicationRepository;

    private readonly IMapper _mapper;

    public ApplicationServices(IApplicationRepository applicationRepository, IMapper mapper)
    {
        _applicationRepository = applicationRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get pagination applications data
    /// </summary>
    /// <param name="paginationParameters">Page size and page number</param>
    /// <returns>Applications paged list</returns>
    public Result<PagedList<ApplicationResponse>> GetPaginate(PaginationParameters paginationParameters)
    {
        var result = _applicationRepository.GetPaginate(paginationParameters);

        if (result.IsFailed)
        {
            return ResultExtensions.Fail(result.Errors);
        }

        var applicationsPagedList = new PagedList<ApplicationResponse>()
        {
            PageSize = result.Value.PageSize,
            CurrentPage = result.Value.CurrentPage,
            TotalCount = result.Value.TotalCount,
            TotalPages = result.Value.TotalPages
        };

        applicationsPagedList.AddRange(result.Value.Select(a => _mapper.Map<ApplicationResponse>(a)));

        return Result.Ok(applicationsPagedList);
    }

    /// <summary>
    /// Create new application
    /// </summary>
    /// <param name="applicationName">Application name (unique name)</param>
    /// <returns>Saved data</returns>
    public async Task<Result<ApplicationResponse>> CreateAsync(string applicationName)
    {
        var application = BuildApplicationEntity(applicationName);

        var result = await _applicationRepository.CreateAsync(application);

        if (result.IsFailed)
        {
            return ResultExtensions.Fail(result.Errors);
        }

        var applicationResponse = _mapper.Map<ApplicationResponse>(result.Value);

        return Result.Ok(applicationResponse);
    }

    /// <summary>
    /// Build application entity
    /// </summary>
    /// <param name="applicationName">Application name (unique name)</param>
    /// <returns>Entity application object</returns>
    public Domain.Application.Application BuildApplicationEntity(string applicationName)
    {
        var who = "ApplicationName";
        var nowUtcDate = DateTime.UtcNow;

        return new Domain.Application.Application
        {
            ApiKey = Guid.NewGuid().ToString(),
            ApplicationName = applicationName,
            CreatedAt = nowUtcDate,
            CreatedBy = who,
            EditedAt = nowUtcDate,
            EditedBy = who,
            ApplicationUsers = new List<Domain.Application.ApplicationUser>()
        };
    }

    /// <summary>
    /// Get application by id
    /// </summary>
    /// <param name="id">Application id</param>
    /// <returns>Application</returns>
    public async Task<Result<ApplicationResponse>> GetByIdAsync(Guid id)
    {
        var result = await _applicationRepository.GetByIdAsync(id);

        if (result.IsFailed)
        {
            return ResultExtensions.Fail(result.Errors);
        }

        var applicationResponse = _mapper.Map<ApplicationResponse>(result.Value);

        return Result.Ok(applicationResponse);
    }

    /// <summary>
    /// Delete application by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Result</returns>
    public async Task<Result<ApplicationResponse>> DeleteAsync(Guid id)
    {
        var result = await _applicationRepository.DeleteAsync(id);

        if (result.IsFailed)
        {
            return ResultExtensions.Fail(result.Errors);
        }

        var applicationResponse = _mapper.Map<ApplicationResponse>(result.Value);

        return Result.Ok(applicationResponse);
    }

    /// <summary>
    /// Update application
    /// </summary>
    /// <param name="applicationRequest">Id, ApiKey and ApplicationName</param>
    /// <returns>Updated application</returns>
    public async Task<Result<ApplicationResponse>> UpdateAsync(ApplicationRequest applicationRequest)
    {
        var result = await _applicationRepository.UpdateAsync(_mapper.Map<Domain.Application.Application>(applicationRequest));

        if (result.IsFailed)
        {
            return ResultExtensions.Fail(result.Errors);
        }

        var applicationResponse = _mapper.Map<ApplicationResponse>(result.Value);

        return Result.Ok(applicationResponse);
    }

    /// <summary>
    /// Validate Apikey by ApplicationName
    /// </summary>
    /// <param name="applicationName">Application unique name</param>
    /// <param name="apiKeyReceived">Apikey received</param>
    /// <returns>True or false</returns>
    public async Task<bool> ValidateApiKeyByAppNameAsync(string applicationName, string apiKeyReceived)
    {
        var result = await _applicationRepository.GetByApplicationNameAsync(applicationName);

        if (result.IsFailed || !result.Value.ApiKey.Equals(apiKeyReceived))
        {
            return false;
        }

        return true;
    }
}
