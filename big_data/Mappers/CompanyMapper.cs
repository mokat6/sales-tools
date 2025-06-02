
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.IdentityModel.Protocols;

namespace big_data.Mappers
{
    static class CompanyMapper
    {

        public static ProtoApi.Company EntityToGrpc(Modelz.Company c)
        {
            var protoCompany = new ProtoApi.Company
            {
                Id = c.Id,
                CompanyName = c.CompanyName ?? "",
                Country = c.Country ?? "",
                City = c.City ?? "",
                FullAddress = c.FullAddress ?? "",
                Website = c.Website ?? "",
                CategoryGoogle = c.CategoryGoogle ?? "",
                // RatingGoogle = (double?)c.RatingGoogle,
                RatedCount = c.RatedCount ?? "",
                GoogleMapsUrl = c.GoogleMapsUrl ?? "",
                // BigFishScore = c.BigFishScore.Value,
                Classification = c.Classification.HasValue
        ? (ProtoApi.CompClassification)c.Classification.Value
        : ProtoApi.CompClassification.Unspecified,
            };

            if (c.BigFishScore.HasValue) protoCompany.BigFishScore = c.BigFishScore.Value;
            if (c.RatingGoogle.HasValue) protoCompany.RatingGoogle = (double)c.RatingGoogle.Value;

            return protoCompany;
        }


        public static Modelz.Company GrpcToEntity(ProtoApi.AddCompanyRequest c)
        {
            var model = new Modelz.Company();

            if (c.HasCompanyName) model.CompanyName = c.CompanyName;
            if (c.HasCountry) model.Country = c.Country;
            if (c.HasCity) model.City = c.City;
            if (c.HasFullAddress) model.FullAddress = c.FullAddress;
            if (c.HasWebsite) model.Website = c.Website;
            if (c.HasCategoryGoogle) model.CategoryGoogle = c.CategoryGoogle;
            if (c.HasRatingGoogle) model.RatingGoogle = (decimal)c.RatingGoogle;
            if (c.HasRatedCount) model.RatedCount = c.RatedCount;
            if (c.HasGoogleMapsUrl) model.GoogleMapsUrl = c.GoogleMapsUrl;
            if (c.HasBigFishScore) model.BigFishScore = c.BigFishScore;
            if (c.HasClassification)
                model.Classification = (Modelz.CompClassification)c.Classification;

            return model;


        }

        public static void UpdateCompany(ProtoApi.UpdateCompanyRequest request, Modelz.Company entity)
        {
            Console.Write("REQUEST -- ");
            Console.WriteLine(request);
            Console.Write("ENTITY -- ");
            Console.WriteLine(entity);
            // if (request.HasCompanyName) entity.CompanyName = request.CompanyName;
            // if (request.HasCountry) entity.Country = request.Country;
            // if (request.HasCity) entity.City = request.City;
            // if (request.HasFullAddress) entity.FullAddress = request.FullAddress;
            // if (request.HasWebsite) entity.Website = request.Website;
            // if (request.HasCategoryGoogle) entity.CategoryGoogle = request.CategoryGoogle;
            // if (request.HasRatingGoogle) entity.RatingGoogle = (decimal)request.RatingGoogle;
            // if (request.HasRatedCount) entity.RatedCount = request.RatedCount;
            // if (request.HasGoogleMapsUrl) entity.GoogleMapsUrl = request.GoogleMapsUrl;
            // if (request.HasBigFishScore) entity.BigFishScore = request.BigFishScore;
            // if (request.HasClassification) entity.Classification = (Modelz.CompClassification)request.Classification;
        }
    }
}