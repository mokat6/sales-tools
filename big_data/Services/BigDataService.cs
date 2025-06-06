using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using big_data.Mappers;

using big_data.Proto;


namespace big_data.Services
{



    public class BigDataService : ProtoApi.BigDataSuckers.BigDataSuckersBase
    {
        private readonly BigDataContext _context;
        private readonly ILogger<BigDataService> _logger;

        public BigDataService(BigDataContext context, ILogger<BigDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<ProtoApi.ListCompaniesResponse> ListCompanies(ProtoApi.ListCompaniesRequest request, ServerCallContext context)
        {
            var companies = await _context.Companiezz.ToListAsync();

            var DtoList = companies.Select(CompanyMapper.EntityToGrpc);
            ProtoApi.ListCompaniesResponse response = new()
            {
                NextCursor = "curse you 123 lol"
            };
            response.Companies.AddRange(DtoList);
            // return Task.FromResult(response);

            return response;

        }

        public override async Task<Empty> DeleteCompany(ProtoApi.DeleteCompanyRequest request, ServerCallContext context)
        {
            Console.WriteLine("id is " + request.Id);
            var entity = await _context.Companiezz.FindAsync(request.Id);
            Console.WriteLine("entity is " + entity);

            if (entity == null) throw new RpcException(new Status(StatusCode.NotFound, "Company not found"));

            _context.Companiezz.Remove(entity);
            await _context.SaveChangesAsync();
            return new Empty();
        }

        public override async Task<ProtoApi.Company> AddCompany(ProtoApi.AddCompanyRequest request, ServerCallContext context)
        {

            var entity = CompanyMapper.GrpcToEntity(request);
            await _context.Companiezz.AddAsync(entity);

            await _context.SaveChangesAsync();

            return CompanyMapper.EntityToGrpc(entity);

        }

        public override async Task<Company> UpdateCompany(UpdateCompanyRequest request, ServerCallContext context)
        {
            var updated = request.Company;
            var paths = request.UpdateMask.Paths;

            var entity = await _context.Companiezz.SingleOrDefaultAsync(company => company.Id == updated.Id);

            if (entity == null) throw new RpcException(new Status(StatusCode.NotFound, "Company not found"));

            CompanyMapper.PatchEntityFromGrpc(updated, paths, entity);

            // _context.Companiezz.Update(entity);  // don't need. The entity is already tracked by EF. when retrieved, same obj here.
            await _context.SaveChangesAsync();

            return CompanyMapper.EntityToGrpc(entity);

        }

        public override async Task<ProtoApi.GetCompanyContactsResponse> GetCompanyContacts(ProtoApi.GetCompanyContactsRequest request, ServerCallContext context)
        {
            var contactsFetched = await _context.ContactsLOL.Where(contact => contact.CompanyId == request.CompanyId).ToListAsync();

            var response = new ProtoApi.GetCompanyContactsResponse();
            response.Contacts.AddRange(contactsFetched.Select(ContactMapper.EntityToGrpcFull));
            return response;

        }

        public override async Task<ProtoApi.Contact> AddContact(ProtoApi.AddContactRequest request, ServerCallContext context)
        {
            var entity = ContactMapper.GrpcToEntity(request);
            await _context.ContactsLOL.AddAsync(entity);
            await _context.SaveChangesAsync();

            return ContactMapper.EntityToGrpcFull(entity);

        }
        public override async Task<Empty> DeleteContact(ProtoApi.DeleteContactRequest request, ServerCallContext context)
        {
            var contact = await _context.ContactsLOL.FindAsync(request.ContactId);
            if (contact == null) throw new RpcException(new Status(StatusCode.NotFound, "Contact not found"));

            _context.ContactsLOL.Remove(contact);
            await _context.SaveChangesAsync();
            return new Empty();

        }

        public override async Task<ProtoApi.Contact> UpdateContact(ProtoApi.UpdateContactRequest request, ServerCallContext context)
        {
            var entity = await _context.ContactsLOL.SingleOrDefaultAsync(contact => contact.Id == request.Id);

            if (entity == null) throw new RpcException(new Status(StatusCode.NotFound, "Contact not found"));

            ContactMapper.PutUpdateContact(request, entity);
            // _context.ContactsLOL.Update(entity); // don't need, tracked entity.
            await _context.SaveChangesAsync();

            return ContactMapper.EntityToGrpcFull(entity);
        }

    }
}