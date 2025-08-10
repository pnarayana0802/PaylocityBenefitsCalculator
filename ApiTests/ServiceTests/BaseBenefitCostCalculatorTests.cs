using Api.Models;
using Api.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.ServiceTests
{
    public class BaseBenefitCostCalculatorTests
    {
        public Mock<IOptions<PaycheckSettings>> PaycheckSettings { get; }

        public BaseBenefitCostCalculatorTests()
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

        [Fact]
        public void CalculateBenefitCost_Returns_Employee_Base_Cost_Benefit_Deduction()
        {
            // Arrange
            var service = new BaseBenefitCostCalculator(PaycheckSettings.Object);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = 100000,
                Dependents = new List<Dependent>()
            };

            // Act
            var result = service.CalculateBenefitCost(employee, DateTime.Now);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(461.54m, result.Amount);
        }
    }
}
