using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public ActionResult<ApiResponse<GetEmployeeDto>> GetEmployeeById(int id)
    {
        var emp = _employeeService.GetEmployeeById(id);
        return emp != null ? Ok(emp) : NotFound(new ApiResponse<GetEmployeeDto> { Error = "Employees not found" });
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet]
    public ActionResult<IEnumerable<GetEmployeeDto>> GetAllEmployees()
    {
        var emp = _employeeService.GetAllEmployees();
        return emp != null ? Ok(emp) : NotFound(new ApiResponse<IEnumerable<GetEmployeeDto>> { Error = "Employees not found" });
    }

    [SwaggerOperation(Summary = "Add new employee")]
    [HttpPost]
    public ActionResult<ApiResponse<GetEmployeeDto>> AddEmployee([FromBody] GetEmployeeDto employee)
    {
        if (employee == null)
        {
            return BadRequest(new ApiResponse<GetEmployeeDto> { Error = "Employee Data is required" });
        }
        var addEmp = _employeeService.AddEmployee(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = addEmp.Id }, new ApiResponse<GetEmployeeDto> { Data = addEmp});
    }

    [SwaggerOperation(Summary = "Delete an employee")]
    [HttpDelete("{id}")]
    public ActionResult DeleteEmployee(int id) 
    {
        var del = _employeeService.DeleteEmployee(id);
        return del ? NoContent() : NotFound(new ApiResponse<GetEmployeeDto> { Error = "Employee not found" });
    }

    [SwaggerOperation(Summary = "Update existing employee")]
    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] GetEmployeeDto employee)
    {
        if (employee == null)
            return BadRequest(new ApiResponse<GetEmployeeDto> { Error = "Employee Data is required" });
        var update = _employeeService.UpdateEmployee(id, employee);
        return update ? NoContent() : NotFound(new ApiResponse<GetEmployeeDto> { Error = "Employee not found" });
    }
}
