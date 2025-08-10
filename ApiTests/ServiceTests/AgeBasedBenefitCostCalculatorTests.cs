using Api.Models;
using Api.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.ServiceTests
{
    public class AgeBasedBenefitCostCalculatorTests
    {
        public AgeBasedBenefitCostCalculatorTests()
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
        public void CalculateBenefitCost_Returns_Zero_Deduction_When_All_Employee_Dependents_Age_Within_DependentThresholdAge_Based_On_Paycheck_Date()
        {
            // Arrange
            var service = new AgeBasedBenefitCostCalculator(PaycheckSettings.Object);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = 100000,
                Dependents = new List<Dependent>
        {
          new() { Id = 1, EmployeeId = 1, FirstName = "Dep" , LastName = "One", DateOfBirth = new DateTime(2020, 01, 01), Relationship = Relationship.Child },
          new() { Id = 2, EmployeeId = 1, FirstName = "Dep" , LastName = "Two", DateOfBirth = new DateTime(2021, 01, 01), Relationship = Relationship.None }
        }
            };

            var paycheckDate = new DateTime(2024, 11, 23);

            // Act
            var result = service.CalculateBenefitCost(employee, paycheckDate);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(0, result.Amount);
        }

        [Fact]
        public void CalculateBenefitCost_Returns_More_Than_Zero_Deduction_When_Employee_Dependents_Age_More_Than_DependentThresholdAge_Based_On_Paycheck_Date()
        {
            // Arrange
            var service = new AgeBasedBenefitCostCalculator(PaycheckSettings.Object);

            var employee = new Employee
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 01, 01),
                Salary = 100000,
                Dependents = new List<Dependent>
        {
          new() { Id = 1, EmployeeId = 1, FirstName = "Dep" , LastName = "One", DateOfBirth = new DateTime(1960, 01, 01), Relationship = Relationship.Child },
          new() { Id = 2, EmployeeId = 1, FirstName = "Dep" , LastName = "Two", DateOfBirth = new DateTime(2021, 01, 01), Relationship = Relationship.None }
        }
            };

            var paycheckDate = new DateTime(2024, 11, 23);

            // Act
            var result = service.CalculateBenefitCost(employee, paycheckDate);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(92.31m, result.Amount);
        }

        public Mock<IOptions<PaycheckSettings>> PaycheckSettings { get; }
    }
}
