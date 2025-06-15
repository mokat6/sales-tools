using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc;
using gatewayRoot.Services;
using Grpc.Core;
using Microsoft.AspNetCore.JsonPatch;

namespace GatewayRoot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly BigDataGrpcClient _bigDataClient;

        public CompaniesController(BigDataGrpcClient bigDataClient)
        {
            _bigDataClient = bigDataClient;
        }

        // if no Name, it will randomly give you name, and randomly rename in the future... needs a stable name.
        // sets operationId = "ListCompanies" in the contract, this is for generated SDK
        [HttpGet(Name = "ListCompanies")]
        public async Task<ActionResult<CompaniesResponseOffset>> GetCompaniesByOffset([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 12)

        {
            // return await _bigDataClient.ListCompaniesAsync(pageSize, cursor);

            var response = await _bigDataClient.ListCompaniesByOffsetAsync(pageIndex, pageSize);

            return Ok(response);
        }

        [HttpGet("cursor", Name = "ListCompaninesWithCursor")]
        public async Task<ActionResult<CompaniesResponseCursor>> GetWithCursor(
            [FromQuery] int pageSize = 12,
            [FromQuery] string? cursor = null
            )
        {
            var response = await _bigDataClient.ListCompaniesWithCursorAsync(pageSize, cursor);
            return Ok(response);
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
        public async Task<ActionResult<CompanyDto>> PatchCompany(long id, [FromBody] JsonPatchDocument<CompanyDto> patchDoc)
        {
            try
            {
                var updatedCompany = await _bigDataClient.PatchCompanyAsync(id, patchDoc);
                return Ok(updatedCompany);
            }
            catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                return NotFound(ex.Status.Detail);
            }
        }

        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<ActionResult<CompanyDto>> GetCompany(long id)
        {
            var companyDto = await _bigDataClient.GetCompany(id);
            if (companyDto == null)
            {
                return NotFound();
            }

            return Ok(companyDto);
        }

    }

}
