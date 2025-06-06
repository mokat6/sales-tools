
using gatewayRoot.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class CompanyMapper
{
    public static CompanyDto ToDto(ProtoApi.Company grpc)
    {
        return new CompanyDto
        {
            Id = grpc.Id,
            CompanyName = grpc.CompanyName,
            Country = grpc.Country,
            City = grpc.City,
            FullAddress = grpc.FullAddress,
            Website = grpc.Website,
            CategoryGoogle = grpc.CategoryGoogle,
            RatingGoogle = grpc.HasRatingGoogle ? grpc.RatingGoogle : null,
            RatedCount = grpc.RatedCount,
            GoogleMapsUrl = grpc.GoogleMapsUrl,
            BigFishScore = grpc.HasBigFishScore ? grpc.BigFishScore : null,
            Classification = (CompClassificationDto)grpc.Classification
        };
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
        if (dto.Classification.HasValue) grpcCompany.Classification = (ProtoApi.CompClassification)dto.Classification.Value;

        return grpcCompany;

    }

}
