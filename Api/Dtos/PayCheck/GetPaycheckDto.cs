namespace Api.Dtos.PayCheck
{
    public class GetPaycheckDto
    {
        public int EmployeeId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
    }
}
