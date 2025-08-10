using Api.Models;
using Api.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.ServiceTests
{
    public class AdditionalBenefitCostCalculatorTests
    {
        public AdditionalBenefitCostCalculatorTests()
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
        [InlineData(1)]
        [InlineData(79999.99)]
        [InlineData(80000)]
        public void CalculateBenefitCost_Returns_Zero_Deduction_When_Employee_Salary_Is_Less_Than_Or_Equal_To__AdditionalBenefitThreshold(decimal salary)
        {
            // Arrange
            var service = new AdditionalBenefitCostCalculator(PaycheckSettings.Object);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = salary,
                Dependents = new List<Dependent>()
            };

            // Act
            var result = service.CalculateBenefitCost(employee, DateTime.Now);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(0, result.Amount);
        }

        [Theory]
        [InlineData(80001, 61.54)]
        [InlineData(90000, 69.23)]
        [InlineData(100000, 76.92)]
        [InlineData(127389.45, 97.99)]
        public void CalculateBenefitCost_Returns_Calculated_BenefitCost_Deduction_When_Employee_Salary_Is_More_Than_AdditionalBenefitThreshold(decimal salary, decimal expectedBenefitDeduction)
        {
            // Arrange
            var service = new AdditionalBenefitCostCalculator(PaycheckSettings.Object);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = salary,
                Dependents = new List<Dependent>()
            };

            // Act
            var result = service.CalculateBenefitCost(employee, DateTime.Now);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(expectedBenefitDeduction, result.Amount);
        }

        public Mock<IOptions<PaycheckSettings>> PaycheckSettings { get; }
    }
}
