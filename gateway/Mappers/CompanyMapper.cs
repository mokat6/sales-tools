
using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class CompanyMapper
{
    public static CompanyDto ToDto(ProtoApi.Company grpc)
    {
        CompanyDto dto = new() { Id = grpc.Id };
        if (grpc.HasCompanyName) dto.CompanyName = grpc.CompanyName;
        if (grpc.HasCountry) dto.Country = grpc.Country;
        if (grpc.HasCity) dto.City = grpc.City;
        if (grpc.HasFullAddress) dto.FullAddress = grpc.FullAddress;
        if (grpc.HasWebsite) dto.Website = grpc.Website;
        if (grpc.HasCategoryGoogle) dto.CategoryGoogle = grpc.CategoryGoogle;
        if (grpc.HasRatingGoogle) dto.RatingGoogle = grpc.RatingGoogle;
        if (grpc.HasRatedCount) dto.RatedCount = grpc.RatedCount;
        if (grpc.HasGoogleMapsUrl) dto.GoogleMapsUrl = grpc.GoogleMapsUrl;
        if (grpc.HasBigFishScore) dto.BigFishScore = grpc.BigFishScore;
        dto.Classification = grpc.Classification.Where(c => c != ProtoApi.CompClassification.Unspecified)
            .Select(c => (CompClassificationDto)c).ToList();

        return dto;
    }

    public static ProtoApi.Company PatchDtoToGrpc(long id, CompanyDto dto)
    {
        var grpcCompany = new ProtoApi.Company() { Id = id };
        // grpcCompany.clear
        // for real objects (obj, string) != null.   For value types like enum, number .HasValue()   when it's nullable.
        // if (dto.CompanyName != null) request.CompanyName = dto.CompanyName;

        // need null checks, because grpcCompany is not nullable
        // if you just assign without `!= null` compiler thinks its OK. because string to string, string can be null
        // but .proto file doesn't say `optional`
        // and the generated from proto class has this setter
        // companyName_ = pb::ProtoPreconditions.CheckNotNull(value, "value"); << throws 


        if (dto.CompanyName != null) grpcCompany.CompanyName = dto.CompanyName;
        if (dto.Country != null) grpcCompany.Country = dto.Country;
        if (dto.City != null) grpcCompany.City = dto.City;
        if (dto.FullAddress != null) grpcCompany.FullAddress = dto.FullAddress;
        if (dto.Website != null) grpcCompany.Website = dto.Website;
        if (dto.CategoryGoogle != null) grpcCompany.CategoryGoogle = dto.CategoryGoogle;
        if (dto.RatingGoogle.HasValue) grpcCompany.RatingGoogle = dto.RatingGoogle.Value;
        if (dto.RatedCount != null) grpcCompany.RatedCount = dto.RatedCount;
        if (dto.GoogleMapsUrl != null) grpcCompany.GoogleMapsUrl = dto.GoogleMapsUrl;
        if (dto.BigFishScore.HasValue) grpcCompany.BigFishScore = dto.BigFishScore.Value;
        if (dto.Classification.Count > 0) grpcCompany.Classification.AddRange(
            dto.Classification.Select(c => (ProtoApi.CompClassification)c).ToList()
            );

        return grpcCompany;

    }

}
