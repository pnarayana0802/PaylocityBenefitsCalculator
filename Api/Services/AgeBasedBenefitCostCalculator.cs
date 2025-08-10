using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class AgeBasedBenefitCostCalculator : BenefitCostCalculator
    {
        public AgeBasedBenefitCostCalculator(IOptions<PaycheckSettings> options)
      : base(options) { }

        public override Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate)
        {
            var overAgeDependentsCount = employee.Dependents.Count(d => Utility.GetAge(d.DateOfBirth, paycheckDate) > Options.DependentThresholdAge);

            return new Deduction
            {
                Amount = Math.Round(overAgeDependentsCount * Options.DependentOver50ExtraMonthlyCost * Options.MonthsPerYear / Options.PayPeriodsPerYear, 2),
                Description = "Dependent age based benefit deduction per paycheck",
            };
        }
    }
}
