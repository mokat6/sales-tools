using BigData;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;


public class BigDataService : BigDataSuckers.BigDataSuckersBase
{
    private readonly BigDataContext _context;
    private readonly ILogger<BigDataService> _logger;

    public BigDataService(BigDataContext context, ILogger<BigDataService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public override async Task<ListCompaniesResponse> ListCompanies(ListCompaniesRequest request, ServerCallContext context)
    {
        var companies = await _context.Companiezz.ToListAsync();

        var DtoList = companies.Select(MapToGrpcCompany);
        ListCompaniesResponse response = new()
        {
            NextCursor = "curse you 123 lol"
        };
        response.Companies.AddRange(DtoList);
        // return Task.FromResult(response);

        return response;

    }

    public override async Task<GetCompanyContactsResponse> GetCompanyContacts(GetCompanyContactsRequest request, ServerCallContext context)
    {
        var contactsFetched = await _context.ContactsLOL.Where(contact => contact.CompanyId == request.CompanyId).ToListAsync();

        var response = new GetCompanyContactsResponse();
        response.Contacts.AddRange(contactsFetched.Select(MapToGrpcCompany));

        return response;

    }


    private static BigData.Company MapToGrpcCompany(big_data.Models.Company c)
    {
        return new BigData.Company
        {
            Id = c.Id,
            CompanyName = c.CompanyName ?? "",
            Country = c.Country ?? "",
            City = c.City ?? "",
            FullAddress = c.FullAddress ?? "",
            Website = c.Website ?? "",
            CategoryGoogle = c.CategoryGoogle ?? "",
            RatingGoogle = (double?)c.RatingGoogle,

            RatedCount = c.RatedCount ?? "",
            GoogleMapsUrl = c.GoogleMapsUrl ?? "",
            BigFishScore = c.BigFishScore,
            Classification = c.Classification.HasValue
    ? (BigData.CompClassification)c.Classification.Value
    : BigData.CompClassification.Unspecified,
        };

    }


    private static BigData.Contact MapToGrpcCompany(big_data.Models.Contact c)
    {
        return new BigData.Contact
        {
            Id = c.Id,
            CompanyId = c.CompanyId,
            Value = c.Value,
            Type = (BigData.ContactType)(int)c.Type,
            IsOnWhatsapp = c.IsOnWhatsApp,
            ContactedFromEmail = c.ContactedFromEmail,
            Checked = c.Checked,
            Date = c.Date.HasValue ? Timestamp.FromDateTime(c.Date.Value.ToUniversalTime()) : null,
            SentFrom = c.SentFrom
        };
    }
}
