using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc;
using gatewayRoot.Services;

namespace GatewayRoot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly BigDataGrpcClient _bigDataClient;

        public CompanyController(BigDataGrpcClient bigDataClient)
        {
            _bigDataClient = bigDataClient;
        }
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> Get([FromQuery] int pageSize = 12, [FromQuery] string? cursor = "")

        {
            return await _bigDataClient.ListCompaniesAsync(pageSize, cursor);
        }



    }

}
