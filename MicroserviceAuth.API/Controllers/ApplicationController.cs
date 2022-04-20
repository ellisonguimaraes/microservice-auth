using MicroserviceAuth.Domain.DTO;
using MicroserviceAuth.Domain.DTO.Application;
using MicroserviceAuth.Domain.Pagination;
using MicroserviceAuth.Service.Application;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MicroserviceAuth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    public readonly IApplicationServices _applicationServices;

    public ApplicationController(IApplicationServices applicationServices)
    {
        _applicationServices = applicationServices;
    }

    /// <summary>
    /// Get pagination data
    /// </summary>
    /// <param name="paginationParameters">Page size and Page number</param>
    /// <returns>Applications paged list</returns>
    /// <response code="200">Successfully obtained</response>
    /// <response code="400">An error has occurred</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<PagedList<ApplicationResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<PagedList<ApplicationResponse>>))]
    public IActionResult GetPaginate([FromQuery]PaginationParameters paginationParameters)
    {
        var result = _applicationServices.GetPaginate(paginationParameters);

        if (result.IsFailed)
        {
            return BadRequest(new GenericResponse<PagedList<ApplicationResponse>>(StatusCodes.Status400BadRequest, null, result.Errors.Select(e => e.Message).ToList()));
        }

        var metadata = new
        {
            result.Value.PageSize,
            result.Value.TotalCount,
            result.Value.CurrentPage,
            result.Value.TotalPages,
            result.Value.HasPrevious,
            result.Value.HasNext
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        return Ok(new GenericResponse<PagedList<ApplicationResponse>>(StatusCodes.Status200OK, result.Value));
    }

    /// <summary>
    /// Get application by id
    /// </summary>
    /// <param name="id" >Application id</param>
    /// <returns>Application</returns>
    /// <response code="200">Successfully obtained</response>
    /// <response code="404">Application not found</response>
    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<ApplicationResponse>))]
    public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id)
    {
        var result = await _applicationServices.GetByIdAsync(id);

        if (result.IsFailed)
        {
            return NotFound(new GenericResponse<ApplicationResponse>(StatusCodes.Status404NotFound, null, result.Errors.Select(e => e.Message).ToList()));
        }

        return Ok(new GenericResponse<ApplicationResponse>(StatusCodes.Status200OK, result.Value));
    }

    /// <summary>
    /// Create new application
    /// </summary>
    /// <param name="applicationName">Application unique name</param>
    /// <returns>Application created</returns>
    /// <response code="201">Application created</response>
    /// <response code="400">An error has occurred</response>
    [HttpPost]
    [Route("{applicationName}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<ApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<ApplicationResponse>))]
    public async Task<IActionResult> PostAsync([FromRoute]string applicationName)
    {
        var result = await _applicationServices.CreateAsync(applicationName);

        if (result.IsFailed)
        {
            return BadRequest(new GenericResponse<ApplicationResponse>(StatusCodes.Status400BadRequest, null, result.Errors.Select(e => e.Message).ToList()));
        }

        return Created($"api/application/{result.Value.Id}", new GenericResponse<ApplicationResponse>(StatusCodes.Status201Created, result.Value));
    }


    /// <summary>
    /// Update application
    /// </summary>
    /// <param name="applicationRequest">Application id, Api key and name</param>
    /// <returns>Application or GenericResponse error</returns>
    /// <response code="200">Application updated</response>
    /// <response code="400">An error has occurred</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<ApplicationResponse>))]
    public async Task<IActionResult> UpdateAsync([FromBody] ApplicationRequest applicationRequest)
    {
        var result = await _applicationServices.UpdateAsync(applicationRequest);

        if (result.IsFailed)
        {
            return BadRequest(new GenericResponse<ApplicationResponse>(StatusCodes.Status400BadRequest, null, result.Errors.Select(e => e.Message).ToList()));
        }

        return Ok(new GenericResponse<ApplicationResponse>(StatusCodes.Status200OK, result.Value));
    }

    /// <summary>
    /// Delete application
    /// </summary>
    /// <param name="id">Application id</param>
    /// <returns>NoContent or GenericResponse error</returns>
    /// <response code="200">Application deleted</response>
    /// <response code="400">An error has occurred</response>
    [HttpDelete]
    [Route("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ApplicationResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<ApplicationResponse>))]
    public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
    {
        var result = await _applicationServices.DeleteAsync(id);

        if (result.IsFailed)
        {
            return BadRequest(new GenericResponse<ApplicationResponse>(StatusCodes.Status400BadRequest, null, result.Errors.Select(e => e.Message).ToList()));
        }

        return Ok(new GenericResponse<ApplicationResponse>(StatusCodes.Status200OK, result.Value));
    }
}
