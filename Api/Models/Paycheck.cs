namespace Api.Models
{
    public class Paycheck
    {
        public int PayPeriod { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay => GrossPay - TotalDeductions;
        public List<Deduction>? Deductions { get; set; }
    }
}
