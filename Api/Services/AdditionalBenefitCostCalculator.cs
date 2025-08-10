using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class AdditionalBenefitCostCalculator : BenefitCostCalculator
    {
        public AdditionalBenefitCostCalculator(IOptions<PaycheckSettings> options)
      : base(options) { }

        public override Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate)
        {
            return new Deduction
            {
                Amount = employee.Salary > Options.SalaryThreshold ? Math.Round(employee.Salary * Options.AdditionalSalaryCostPercent / Options.PayPeriodsPerYear, 2) : 0.0m,
                Description = "Employee salary additional benefit deduction per paycheck",
            };
        }
    }
}
