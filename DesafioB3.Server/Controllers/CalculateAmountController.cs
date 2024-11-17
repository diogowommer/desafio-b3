using DesafioB3.API.Features.CalculateAmount;
using Microsoft.AspNetCore.Mvc;

namespace DesafioB3.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateAmountController : ControllerBase
    {
        private readonly ILogger<CalculateAmountController> _logger;

        public CalculateAmountController(ILogger<CalculateAmountController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CalculateAmount")]
        public CalculateAmountResponse Post(CalculateAmountRequest request)
        {
            decimal grossAmount = request.InitialValue * request.Quantity;
            decimal netAmount = (request.InitialValue * request.Quantity) - 100;

            return new CalculateAmountResponse(grossAmount, netAmount);
        }
    }
}
