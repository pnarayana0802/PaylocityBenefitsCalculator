using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using AutoMapper;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IMapper _mapper;
        public static readonly List<Employee> _employees = new();

        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            var empList = new List<GetEmployeeDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<GetDependentDto>
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<GetDependentDto>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                }
            };
            _employees.Clear();
            _employees.AddRange(_mapper.Map<List<Employee>>(empList));

            return empList;
        }

        public GetEmployeeDto? GetEmployeeById(int id)
        {
            var employees = _employees.FirstOrDefault(e => e.Id == id);
            return employees == null ? null : _mapper.Map<GetEmployeeDto>(employees);
        }

        public GetEmployeeDto AddEmployee(GetEmployeeDto getEmployeeDto)
        {
            var emp = _mapper.Map<Employee>(getEmployeeDto);
            emp.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(emp);
            return _mapper.Map<GetEmployeeDto>(emp);
        }

        public bool UpdateEmployee(int id, GetEmployeeDto getEmployeeDto)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) {
                return false;
            }
            _mapper.Map<GetEmployeeDto>(emp);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return false;
            _employees.Remove(emp);
            return true;
        }
    }
}
