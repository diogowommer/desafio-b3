using DesafioB3.Server.Model;
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
            CalculateAmountResponse response = new CalculateAmountResponse()
            {
                GrossAmount = request.InitialValue * request.Quantity,
                NetAmount = (request.InitialValue * request.Quantity) - 100
            };

            return response;
        }
    }
}
