using MicroserviceAuth.Domain.Application;
using MicroserviceAuth.Domain.DTO;
using MicroserviceAuth.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceAuth.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    
    public readonly IRepository<Application> _applicationRepository;

    public TestController(IRepository<Application> applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var all = await _applicationRepository.GetAllAsync();
        return Ok(new GenericResponse(StatusCodes.Status200OK, all));
    }
}
