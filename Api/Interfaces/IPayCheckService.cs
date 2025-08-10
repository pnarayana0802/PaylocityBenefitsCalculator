using Api.Dtos.PayCheck;
using Microsoft.AspNetCore.Mvc;

namespace Api.Interfaces
{
    public interface IPayCheckService
    {
        Task<GetPaycheckDto?> CalculatePaycheck(int empId, [FromQuery] DateTime paycheckDate);
    }
}
