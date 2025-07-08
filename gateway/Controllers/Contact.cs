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
        [HttpGet(Name = "GetCompanyContacts")]
        public async Task<IEnumerable<ContactDto>> Get([FromQuery] long compId)

        {
            return await _bigDataClient.GetCompanyContactsAsync(compId);
        }

        // TODO: check this out
        //  [HttpPost("contactsLOL")]  // sets the route, adds to base, can I have both?

        [HttpPost(Name = "CreateCompanyContact")]
        public async Task<ActionResult<ContactDto>> Post([FromBody] CreateContactDto dto)
        {
            var response = await _bigDataClient.CreateContact(dto);

            return Ok(response);
        }



    }

}
