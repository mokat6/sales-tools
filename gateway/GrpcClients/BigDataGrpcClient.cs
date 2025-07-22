// Need to catch errors when calling _client. methods
// Because gRPC server throws errors when not found by id, etc

using ProtoApi = big_data.Proto;

using Grpc.Net.Client;
using gatewayRoot.Dtos;
using gatewayRoot.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using System.Dynamic;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Grpc.Core;
using big_data.Proto;

namespace gatewayRoot.Services
{
    public class BigDataGrpcClient
    {
        private readonly ProtoApi.BigDataProtoService.BigDataProtoServiceClient _client;

        public BigDataGrpcClient(ProtoApi.BigDataProtoService.BigDataProtoServiceClient client)
        {
            _client = client;
        }

        public async Task<CompaniesResponseOffset> ListCompaniesByOffsetAsync(int pageIndex, int pageSize)
        {
            var request = new ProtoApi.ListCompaniesByOffsetRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var protoResponse = await _client.ListCompaniesByOffsetAsync(request);

            var pagination = new PaginationDto
            {
                PageIndex = protoResponse.PageIndex,
                PageSize = protoResponse.PageSize,
                TotalCount = protoResponse.TotalCount,
            };
            var restResponse = new CompaniesResponseOffset
            {
                Companies = protoResponse.Companies.Select(CompanyMapper.ToDto),
                Pagination = pagination

            };
            return restResponse;
        }

        public async Task<CompaniesResponseCursor> ListCompaniesWithCursorAsync(int? pageSize, string? cursor, string? search, string? sortBy, string? sortDirection, bool isDownloadAll)
        {
            var rpcRequest = new ListCompaniesWithCursorRequest();

            if (pageSize.HasValue) rpcRequest.PageSize = pageSize.Value;
            if (cursor != null) rpcRequest.Cursor = cursor;
            if (search != null) rpcRequest.Search = search;
            if (sortBy != null) rpcRequest.SortBy = sortBy;
            if (sortDirection != null) rpcRequest.SortDirection = sortDirection;
            rpcRequest.IsDownloadAll = isDownloadAll;

            var protoResponse = await _client.ListCompaniesWithCursorAsync(rpcRequest);
            var pagination = new PaginationCursorDto(protoResponse.TotalCount,
            protoResponse.HasNextCursor ? protoResponse.NextCursor : null
            );

            var restResponse = new CompaniesResponseCursor
            {
                Companies = protoResponse.Companies.Select(CompanyMapper.ToDto),
                Pagination = pagination
            };

            return restResponse;
        }


        public async Task DeleteCompanyAsync(long id)
        {
            Console.WriteLine(id);
            var request = new ProtoApi.DeleteCompanyRequest()
            {
                Id = id
            };

            await _client.DeleteCompanyAsync(request);
        }

        // to return Task is same as  return `204 No Content`
        public async Task<CompanyDto> PatchCompanyAsync(long id, JsonPatchDocument<CompanyDto> patchDoc)
        {
            Console.WriteLine("GATEWAY grpc client start");
            var dto = new CompanyDto();
            patchDoc.ApplyTo(dto);
            Console.WriteLine("GGG before creating company object");
            Console.Write("the dto  >>> "); Console.WriteLine(JsonConvert.SerializeObject(dto));
            var company = CompanyMapper.PatchDtoToGrpc(id, dto);
            Console.Write("After patch applied: "); Console.WriteLine(company);
            Console.WriteLine("PATH DOC HERE!");
            Console.WriteLine(JsonConvert.SerializeObject(patchDoc.Operations, Formatting.Indented));


            var strategy = new SnakeCaseNamingStrategy();

            var fieldMask = new FieldMask
            {
                Paths = {
        patchDoc.Operations
            .Select(op => string.Join(".", op.path.TrimStart('/').Split('/').Select(p => strategy.GetPropertyName(p, false))))
            .Distinct()
    }
            };

            Console.WriteLine(fieldMask);


            var grpcRequest = new ProtoApi.UpdateCompanyRequest { Company = company, UpdateMask = fieldMask };

            var returnedCompany = await _client.UpdateCompanyAsync(grpcRequest);
            return CompanyMapper.ToDto(returnedCompany);
        }

        public async Task<List<ContactDto>> GetCompanyContactsAsync(long compId)
        {
            var request = new ProtoApi.GetCompanyContactsRequest()
            {
                CompanyId = compId
            };
            var response = await _client.GetCompanyContactsAsync(request);

            return [.. response.Contacts.Select(ContactMapper.GrpcToDto)];

        }
        public async Task<CompanyDto?> GetCompany(long compId)
        {
            var request = new ProtoApi.GetCompanyRequest { Id = compId };
            try
            {
                var response = await _client.GetCompanyAsync(request);
                return CompanyMapper.ToDto(response);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                return null; // company not found - return null. it should be like that! REST API has if (null) return 404
            }
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto dto)
        {
            ProtoApi.AddContactRequest request = ContactMapper.createRequestDtoToGrpc(dto);

            var response = await _client.AddContactAsync(request);

            return Mappers.ContactMapper.GrpcToDto(response);
        }

        public async Task<Empty> DeleteContactAsync(long contactId)
        {
            ProtoApi.DeleteContactRequest request = new()
            {
                ContactId = contactId
            };

            var response = await _client.DeleteContactAsync(request);
            return response;
        }

        public async Task<ContactDto> UpdateContactAsync(ContactDto dto)
        {
            var request = ContactMapper.updateRequestDtoToGrpc(dto);
            var response = await _client.UpdateContactAsync(request);

            return ContactMapper.GrpcToDto(response);
        }
    }


}
