using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc;
using gatewayRoot.Services;
using Grpc.Core;
using Microsoft.AspNetCore.JsonPatch;

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
        /// <summary>
        /// Applies a JSON Patch to a company.
        /// </summary>
        /// <remarks>
        /// The request body must follow the <a href="https://tools.ietf.org/html/rfc6902">JSON Patch (RFC 6902)</a> format:
        ///
        /// ```json
        /// [
        ///   { "op": "replace", "path": "/name", "value": "Acme Inc." },
        ///   { "op": "remove", "path": "/isObsolete" }
        /// ]
        /// ```
        ///
        /// **Note:** Ignore `operationType` in the schema — it's an internal property and not required.
        /// </remarks>
        [HttpPatch("{id}", Name = "PatchCompany")]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> PatchCompany(long id, [FromBody] JsonPatchDocument<CompanyDto> patchDoc)
        {
            try
            {
                await _bigDataClient.PatchCompanyAsync(id, patchDoc);
                return NoContent();
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return NotFound(ex.Status.Detail);
            }
        }

    }

}
