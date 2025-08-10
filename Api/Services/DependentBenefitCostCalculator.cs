using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class DependentBenefitCostCalculator : BenefitCostCalculator
    {
        public DependentBenefitCostCalculator(IOptions<PaycheckSettings> options)
      : base(options) { }

        public override Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate)
        {
            return new Deduction
            {
                Amount = Math.Round(employee.Dependents.Count() * Options.DependentMonthlyCost * Options.MonthsPerYear / Options.PayPeriodsPerYear, 2),
                Description = "Dependent base cost benefit deduction per paycheck",
            };
        }
    }
}
