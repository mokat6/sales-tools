
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;

namespace big_data.Mappers
{
    static class CompanyMapper
    {

        public static ProtoApi.Company EntityToGrpc(Modelz.Company c)
        {
            return new ProtoApi.Company
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
        ? (ProtoApi.CompClassification)c.Classification.Value
        : ProtoApi.CompClassification.Unspecified,
            };

        }




    }
}