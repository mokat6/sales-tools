using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc;
using gatewayRoot.Services;

namespace GatewayRoot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly BigDataGrpcClient _bigDataClient;

        public ContactController(BigDataGrpcClient bigDataClient)
        {
            _bigDataClient = bigDataClient;
        }
        [HttpGet]
        public async Task<IEnumerable<ContactDto>> Get([FromQuery] long compId)

        {
            return await _bigDataClient.GetCompanyContactsAsync(compId);
        }



    }

}
