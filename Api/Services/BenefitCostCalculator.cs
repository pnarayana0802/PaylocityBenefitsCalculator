using Api.Interfaces;
using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public abstract class BenefitCostCalculator : IBenefitCostCalculator
    {
        public abstract Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate);

        protected PaycheckSettings Options { get; }

        protected BenefitCostCalculator(IOptions<PaycheckSettings> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            Options = options.Value;
        }
    }
}
