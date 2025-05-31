using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc;
using gatewayRoot.Services;
using Grpc.Core;

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

        // if no Name, it will randomly give you name, and randomly rename in the future... needs a stable name.
        // sets operationId = "ListCompanies" in the contract
        [HttpGet(Name = "ListCompanies")]
        public async Task<IEnumerable<CompanyDto>> Get([FromQuery] int pageSize = 12, [FromQuery] string? cursor = "")

        {
            return await _bigDataClient.ListCompaniesAsync(pageSize, cursor);
        }

        [HttpDelete("{id}", Name = "DeleteCompany")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _bigDataClient.DeleteCompanyAsync(id);
                return NoContent(); // 204
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return NotFound(); // 404
            }
            catch (RpcException ex)
            {
                // Other gRPC errors (optional)
                return StatusCode(500, $"gRPC error: {ex.Status.Detail}");
            }
        }

        [HttpPatch("{id}", Name = "PatchCompany")]
        public async Task<IActionResult> PatchCompany(long id, [FromBody] PatchCompanyDto patchCompanyDto)
        {
            try
            {
                await _bigDataClient.PatchCompanyAsync(id, patchCompanyDto);
                return NoContent();
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return NotFound(ex.Status.Detail);
            }
        }

    }

}
