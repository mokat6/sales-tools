
using big_data.Proto;
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

    public static UpdateCompanyRequest ToUpdateCompanyRequest(long id, PatchCompanyDto dto)
    {
        var request = new UpdateCompanyRequest { Id = id };

        // for real objects (obj, string) != null.   For value types like enum, number .HasValue()   when it's nullable.
        if (dto.CompanyName != null) request.CompanyName = dto.CompanyName;
        if (dto.Country != null) request.Country = dto.Country;
        if (dto.City != null) request.City = dto.City;
        if (dto.FullAddress != null) request.FullAddress = dto.FullAddress;
        if (dto.Website != null) request.Website = dto.Website;
        if (dto.CategoryGoogle != null) request.CategoryGoogle = dto.CategoryGoogle;
        if (dto.RatingGoogle.HasValue) request.RatingGoogle = dto.RatingGoogle.Value;
        if (dto.RatedCount != null) request.RatedCount = dto.RatedCount;
        if (dto.GoogleMapsUrl != null) request.GoogleMapsUrl = dto.GoogleMapsUrl;
        if (dto.BigFishScore.HasValue) request.BigFishScore = dto.BigFishScore.Value;
        if (dto.Classification.HasValue) request.Classification = dto.Classification.Value;

        return request;

    }

}
