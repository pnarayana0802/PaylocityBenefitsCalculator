using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class BaseBenefitCostCalculator : BenefitCostCalculator
    {
        public BaseBenefitCostCalculator(IOptions<PaycheckSettings> options)
      : base(options) { }

        public override Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate)
        {
            return new Deduction
            {
                Amount = Math.Round(Options.BaseMonthlyCost * Options.MonthsPerYear / Options.PayPeriodsPerYear, 2),
                Description = "Employee base cost benefit deduction per paycheck",
            };
        }    
    }
}
