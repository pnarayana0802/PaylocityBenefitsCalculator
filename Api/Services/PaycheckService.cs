using Api.Dtos.PayCheck;
using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class PaycheckService : IPayCheckService
    {
        private readonly PaycheckSettings _settings;
        private static readonly List<Employee> _employees = new();
        private readonly IEnumerable<IBenefitCostCalculator> _benefitCostCalculatorServices;
        private readonly IMapper _mapper;

        public PaycheckService(IOptions<PaycheckSettings> options, IEnumerable<IBenefitCostCalculator> benefitCost, IMapper mapper)
        {
            _settings = options.Value;
            _benefitCostCalculatorServices = benefitCost;
            _mapper = mapper;
        }

        public async Task<GetPaycheckDto> CalculatePaycheck(int empId, [FromQuery] DateTime paycheckDate)
        {            
            var employee = _employees.FirstOrDefault(e => e.Id == empId);
            if (employee == null)
                return null;

            var grossPay = Math.Round(employee.Salary / _settings.PayPeriodsPerYear, 2);

            var deductions = new List<Deduction>();

            foreach (var service in _benefitCostCalculatorServices)
            {
                deductions.Add(service.CalculateBenefitCost(employee, paycheckDate));
            }

            var totalDeduction = Math.Round(deductions.Sum(d => d.Amount), 2);

            var netpay = grossPay - totalDeduction;

            var paycheck = _mapper.Map<GetPaycheckDto>((employee, grossPay, deductions, totalDeduction, netpay));

            return paycheck;
        }
    }
}
