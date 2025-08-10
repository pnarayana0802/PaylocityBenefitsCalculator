using Api.Models;
using Api.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.ServiceTests
{
    public class DependentBenefitCostCalculatorTests
    {
        public DependentBenefitCostCalculatorTests()
        {
            PaycheckSettings = new Mock<IOptions<PaycheckSettings>>();

            PaycheckSettings.Setup(x => x.Value)
              .Returns(new PaycheckSettings
              {
                  MonthsPerYear = Constants.MonthsPerYear,
                  PayPeriodsPerYear = Constants.PaychecksPerYear,
                  BaseMonthlyCost = Constants.EmployeeBaseCostPerMonth,
                  DependentMonthlyCost = Constants.DependentBaseCostPerMonth,
                  SalaryThreshold = Constants.AdditionalBenefitThreshold,
                  AdditionalSalaryCostPercent = Constants.AdditionalBenefitPercentage,
                  DependentThresholdAge = Constants.DependentThresholdAge,
                  DependentOver50ExtraMonthlyCost = Constants.DependentThresholdAgeDeductionPerMonth
              });
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 276.92)]
        [InlineData(2, 553.85)]
        [InlineData(3, 830.77)]
        [InlineData(10, 2769.23)]
        public void CalculateBenefitCost_Returns_Dependent_Base_Cost_Benefit_Deduction_Based_On_Number_Of_Dependents(int dependentCount, decimal expectedAmount)
        {
            // Arrange
            var service = new DependentBenefitCostCalculator(PaycheckSettings.Object);

            var dependentsList = new List<Dependent>();

            for (int i = 1; i <= dependentCount; i++)
            {
                var dependent = new Dependent { Id = i, EmployeeId = 1, DateOfBirth = new DateTime(2024, 01, 01), FirstName = "Dep" + i, LastName = "Dep" + i, Relationship = Relationship.Child };

                dependentsList.Add(dependent);
            }

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = 100000,
                Dependents = dependentsList
            };

            // Act
            var result = service.CalculateBenefitCost(employee, DateTime.Now);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(expectedAmount, result.Amount);
        }

        public Mock<IOptions<PaycheckSettings>> PaycheckSettings { get; }
    }
}
