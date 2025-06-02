// File: Services/BigDataGrpcClient.cs
using ProtoApi = big_data.Proto;

using Grpc.Net.Client;
using gatewayRoot.Dtos;
using gatewayRoot.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using System.Dynamic;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace gatewayRoot.Services
{
    public class BigDataGrpcClient
    {
        private readonly ProtoApi.BigDataSuckers.BigDataSuckersClient _client;

        public BigDataGrpcClient(ProtoApi.BigDataSuckers.BigDataSuckersClient client)
        {
            _client = client;
        }

        public async Task<List<CompanyDto>> ListCompaniesAsync(int pageSize, string? cursor = "")
        {
            var request = new ProtoApi.ListCompaniesRequest()
            {
                PageSize = pageSize,
                Cursor = cursor ?? ""
            };

            var response = await _client.ListCompaniesAsync(request);
            return [.. response.Companies.Select(CompanyMapper.ToDto)];
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

        // return Task is return `204 No Content`
        public async Task PatchCompanyAsync(long id, JsonPatchDocument<PatchCompanyDto> patchDoc)
        {
            Console.WriteLine("GATEWAY grpc client start");
            var dto = new PatchCompanyDto();
            patchDoc.ApplyTo(dto);
            Console.WriteLine("GGG before creating company object");
            var company = CompanyMapper.PatchDtoToGrpc(id, dto);

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

            await _client.UpdateCompanyAsync(grpcRequest);

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
    }
}
