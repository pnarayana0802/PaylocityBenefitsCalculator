using Api.Dtos.Employee;

namespace Api.Interfaces
{
    public interface IEmployeeService
    {
        GetEmployeeDto? GetEmployeeById(int id);
        Task<List<GetEmployeeDto>> GetAllEmployees();
        GetEmployeeDto AddEmployee(GetEmployeeDto employee);
        bool UpdateEmployee(int id, GetEmployeeDto employee);
        bool DeleteEmployee(int id);
    }
}
