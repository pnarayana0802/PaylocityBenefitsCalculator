namespace Api.Models
{
    public class PaycheckSettings
    {
        public decimal BaseMonthlyCost { get; set; }
        public decimal DependentMonthlyCost { get; set; }
        public decimal DependentOver50ExtraMonthlyCost { get; set; }
        public decimal SalaryThreshold { get; set; }
        public decimal AdditionalSalaryCostPercent { get; set; }
        public int DependentThresholdAge { get; set; }
        public decimal PayPeriodsPerYear { get; set; }
        public decimal MonthsPerYear { get; set; }
    }
}
