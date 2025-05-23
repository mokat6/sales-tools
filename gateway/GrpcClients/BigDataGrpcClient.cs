// File: Services/BigDataGrpcClient.cs
using ProtoApi = big_data.Proto;

using Grpc.Net.Client;
using gatewayRoot.Dtos;
using gatewayRoot.Mappers;

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
