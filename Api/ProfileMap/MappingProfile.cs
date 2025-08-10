using Api.Dtos.Employee;
using Api.Dtos.PayCheck;
using Api.Models;
using AutoMapper;

namespace Api.ProfileMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, GetEmployeeDto>().ReverseMap();
            CreateMap<Dependent, GetEmployeeDto>().ReverseMap();
            CreateMap<Paycheck, GetPaycheckDto>().ReverseMap();
        }
    }
}
