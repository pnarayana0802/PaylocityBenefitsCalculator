using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet()]
    public ActionResult<ApiResponse<GetDependentDto>> GetAllDependents()
    {
        var dep = _dependentService.GetAllDependents();
        return dep != null ? Ok(dep) : NotFound(new ApiResponse<IEnumerable<GetDependentDto>> { Error = "Dependents not found" });
    }

    [SwaggerOperation(Summary = "Get dependent for an employee")]
    [HttpGet("{id}")]
    public ActionResult<GetDependentDto> GetDependentById(int id)
    {
        var dep = _dependentService.GetDependentById(id);
        return dep != null ? Ok(dep) : NotFound(new ApiResponse<GetDependentDto> { Error = "Dependents not found" });
    }

    [SwaggerOperation(Summary = "Add new dependent")]
    [HttpPost]
    public ActionResult<ApiResponse<GetDependentDto>> AddDependent([FromBody] GetDependentDto addDependentDto)
    {
        if (addDependentDto == null)
        {
            return BadRequest(new ApiResponse<GetDependentDto> { Error = "Dependent Data is required" });
        }
        var addDep = _dependentService.AddDependentAsync(addDependentDto);
        return CreatedAtAction(nameof(GetDependentById), new { id = addDep.Id }, new ApiResponse<GetDependentDto> { Data = addDep });
    }

    [SwaggerOperation(Summary = "Update existing dependent")]
    [HttpPut("{id}")]
    public IActionResult UpdateDependent(int id, [FromBody] GetDependentDto dependentDto)
    {
        if (dependentDto == null)
            return BadRequest(new ApiResponse<GetEmployeeDto> { Error = "Dependent Data is required" });
        var update = _dependentService.UpdateDependentAsync(id, dependentDto);
        return update ? NoContent() : NotFound(new ApiResponse<GetDependentDto> { Error = "Dependent not found" });
    }

    [SwaggerOperation(Summary = "Delete dependent")]
    [HttpDelete("{id}")]
    public IActionResult DeleteDependent(int id)
    {
        var del = _dependentService.DeleteDependentAsync(id);
        return del ? NoContent() : NotFound(new ApiResponse<GetDependentDto> { Error = "Dependent not found" });
    }
}
