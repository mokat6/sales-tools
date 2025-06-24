using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using big_data.Mappers;

using big_data.Proto;
using Azure;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using big_data.Dtos;
using System.Text.Json;


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

        public override async Task<ListCompaniesByOffsetResponse> ListCompaniesByOffset(ListCompaniesByOffsetRequest request, ServerCallContext context)
        {
            var pageIndex = request.PageIndex;
            var pageSize = request.PageSize;

            // var skip = (page - 1) * pageSize; // starts with page 1
            var skip = pageIndex * pageSize; // starts with page 0, Tanstack table starts with page 0

            var totalCount = await _context.Companiezz.CountAsync();

            var companies = await _context.Companiezz
              .OrderBy(c => c.Id)
              .Skip(skip)
              .Take(pageSize)
              .ToListAsync();
            // Runs in memory, not in SQL. Don't mix it with EF SQL chain .OrderBy().Skip().Take().Select(), wrong.
            var grpcCompanies = companies.Select(CompanyMapper.EntityToGrpc).ToList();



            var response = new Proto.ListCompaniesByOffsetResponse
            {
                PageIndex = pageIndex,
                TotalCount = totalCount,
                PageSize = pageSize,

            };
            // class generated from protobuf, repeated field (the list), has getter, but no setter. Read only.
            // type is not List<Company>, but RepeatedField<Company>
            response.Companies.AddRange(grpcCompanies);
            return response;
        }

        public override async Task<ListCompaniesWithCursorResponse> ListCompaniesWithCursor(ListCompaniesWithCursorRequest request, ServerCallContext context)
        {
            int pageSize = request.HasPageSize ? request.PageSize : 10;
            string search = request.Search;

            string? cursor = request.Cursor;
            string? nameCursor = null;
            long? idCursor = null;

            // Decode cursor if provided
            if (!string.IsNullOrEmpty(cursor))
            {
                try
                {
                    var decoded = Base64UrlEncoder.Decode(cursor);
                    var cursorObj = JsonSerializer.Deserialize<CursorDto>(decoded);
                    nameCursor = cursorObj?.Name;
                    idCursor = cursorObj?.Id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid cursor: " + ex.Message);
                }
            }

            // EF query
            var query = _context.Companiezz.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c =>
                            c.CompanyName != null && c.CompanyName.Contains(search));
            }

            if (!string.IsNullOrEmpty(nameCursor) && idCursor.HasValue)
            {
                query = query.Where(c =>
                    string.Compare(c.CompanyName, nameCursor) > 0 ||   // c.CompanyName > nameCursor  (c# doesn't allow comparing strings with `>`)
                    (c.CompanyName == nameCursor && c.Id > idCursor.Value));
            }

            var companies = await query
                .OrderBy(c => c.CompanyName)
                .ThenBy(c => c.Id)
                .Take(pageSize)
                .ToListAsync();


            // Build next cursor if there’s a next page
            string? nextCursor = null;
            if (companies.Count == pageSize)
            {
                var last = companies[^1];
                var cursorObj = new CursorDto
                {
                    Name = last.CompanyName,
                    Id = last.Id
                };
                var rawCursor = JsonSerializer.Serialize(cursorObj);
                nextCursor = Base64UrlEncoder.Encode(rawCursor);

            }

            // TODO: cache totalCount, expensive operation
            // var totalCount = await _context.Companiezz.CountAsync();
            var totalCount = await query.CountAsync(); // applies search


            return new ListCompaniesWithCursorResponse
            {
                Companies = { companies.Select(CompanyMapper.EntityToGrpc) },
                NextCursor = nextCursor ?? "",
                TotalCount = totalCount
            };
        }



        public override async Task<big_data.Proto.Company> GetCompany(big_data.Proto.GetCompanyRequest request, ServerCallContext context)
        {
            var entity = await _context.Companiezz.FindAsync(request.Id);
            if (entity == null) throw new RpcException(new Status(StatusCode.NotFound, "Company not found"));

            return CompanyMapper.EntityToGrpc(entity);
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