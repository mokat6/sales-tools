
using gatewayRoot.Dtos;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class CompanyMapper
{
    public static CompanyDto ToDto(ProtoApi.Company company)
    {
        return new CompanyDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            Country = company.Country,
            City = company.City,
            FullAddress = company.FullAddress,
            Website = company.Website,
            CategoryGoogle = company.CategoryGoogle,
            RatingGoogle = company.HasRatingGoogle ? company.RatingGoogle : null,
            RatedCount = company.RatedCount,
            GoogleMapsUrl = company.GoogleMapsUrl,
            BigFishScore = company.HasBigFishScore ? company.BigFishScore : null,
            Classification = company.Classification
        };
    }

}
