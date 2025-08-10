using Api.Dtos.PayCheck;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PaycheckController : ControllerBase
    {
        private readonly IPayCheckService? _paycheckService;

        public PaycheckController(IPayCheckService? paycheckService)
        {
            _paycheckService = paycheckService;
        }

        [HttpGet("{id}/Paycheck")]
        public ActionResult<GetPaycheckDto> CalculatePaycheck(int id, [FromQuery] DateTime? paycheckDate)
        {
            var paycheck = _paycheckService?.CalculatePaycheck(id, paycheckDate ?? DateTime.Today);
            return paycheck == null ? NotFound() : Ok(paycheck);
        }
    }
}
