using Api.Models;

namespace Api.Interfaces
{
    public interface IBenefitCostCalculator
    {
        Deduction CalculateBenefitCost(Employee employee, DateTime paycheckDate);
    }
}
