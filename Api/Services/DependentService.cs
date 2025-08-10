using Api.Dtos.Dependent;
using Api.Interfaces;
using Api.Models;
using AutoMapper;

namespace Api.Services
{
    public class DependentService : IDependentService
    {
        private readonly List<Dependent> _dependents = new();
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public DependentService(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public async Task<List<GetDependentDto>?> GetAllDependents()
        {
            var emp = await _employeeService.GetAllEmployees();
            var deps = emp.Where(e => e.Dependents != null)
                           .SelectMany(e => e.Dependents!)
                           .ToList();
            _dependents.Clear();
            _dependents.AddRange(_mapper.Map<List<Dependent>>(deps));

            return deps;
        }

        public GetDependentDto? GetDependentById(int depId)
        {
            var dep = _dependents.FirstOrDefault(e => e.Id == depId);
            if (dep == null)
                return null;
            return _mapper.Map<GetDependentDto>(dep);
        }

        public GetDependentDto AddDependentAsync(GetDependentDto dependentDto)
        {
            var dep = _mapper.Map<Dependent>(dependentDto);
            dep.Id = _dependents.Count > 0 ? _dependents.Max(e => e.Id) + 1 : 1;
            _dependents.Add(dep);
            return _mapper.Map<GetDependentDto>(dep);
        }


        public bool UpdateDependentAsync(int id, GetDependentDto dependentDto)
        {
            var dep = _dependents.FirstOrDefault(e => e.Id == id);
            if (dep == null)
            {
                return false;
            }
            _mapper.Map<GetDependentDto>(dep);
            return true;
        }

        public bool DeleteDependentAsync(int id)
        {
            var emp = _dependents.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return false;
            _dependents.Remove(emp);
            return true;
        }
    }
}
