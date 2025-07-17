
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.IdentityModel.Protocols;
using Grpc.Core;

namespace big_data.Mappers
{
    static class CompanyMapper
    {

        public static ProtoApi.Company EntityToGrpc(Modelz.Company entity)
        {
            var protoCompany = new ProtoApi.Company { Id = entity.Id };

            if (entity.CompanyName != null) protoCompany.CompanyName = entity.CompanyName;
            if (entity.Country != null) protoCompany.Country = entity.Country;
            if (entity.City != null) protoCompany.City = entity.City;
            if (entity.FullAddress != null) protoCompany.FullAddress = entity.FullAddress;
            if (entity.Website != null) protoCompany.Website = entity.Website;
            if (entity.CategoryGoogle != null) protoCompany.CategoryGoogle = entity.CategoryGoogle;
            if (entity.RatedCount != null) protoCompany.RatedCount = entity.RatedCount;
            if (entity.GoogleMapsUrl != null) protoCompany.GoogleMapsUrl = entity.GoogleMapsUrl;
            if (entity.MarkdownNote != null) protoCompany.MarkdownNote = entity.MarkdownNote;
            if (entity.Classification.HasValue)
            {
                var flags = entity.Classification.Value;

                protoCompany.Classification.AddRange(
                    System.Enum.GetValues<Modelz.CompClassification>()
                        .Where(flag => flag != Modelz.CompClassification.Unspecified && flags.HasFlag(flag))
                        .Select(flag => (ProtoApi.CompClassification)flag)
                );
            }
            // if (entity.Classification.HasValue) protoCompany.Classification = (ProtoApi.CompClassification)entity.Classification.Value;

            if (entity.BigFishScore.HasValue) protoCompany.BigFishScore = entity.BigFishScore.Value;
            if (entity.RatingGoogle.HasValue) protoCompany.RatingGoogle = (double)entity.RatingGoogle.Value;


            return protoCompany;
        }


        public static Modelz.Company GrpcToEntity(ProtoApi.AddCompanyRequest protoCompany)
        {
            var entity = new Modelz.Company();

            if (protoCompany.HasCompanyName) entity.CompanyName = protoCompany.CompanyName;
            if (protoCompany.HasCountry) entity.Country = protoCompany.Country;
            if (protoCompany.HasCity) entity.City = protoCompany.City;
            if (protoCompany.HasFullAddress) entity.FullAddress = protoCompany.FullAddress;
            if (protoCompany.HasWebsite) entity.Website = protoCompany.Website;
            if (protoCompany.HasCategoryGoogle) entity.CategoryGoogle = protoCompany.CategoryGoogle;
            if (protoCompany.HasRatingGoogle) entity.RatingGoogle = (decimal)protoCompany.RatingGoogle;
            if (protoCompany.HasRatedCount) entity.RatedCount = protoCompany.RatedCount;
            if (protoCompany.HasGoogleMapsUrl) entity.GoogleMapsUrl = protoCompany.GoogleMapsUrl;
            if (protoCompany.HasBigFishScore) entity.BigFishScore = protoCompany.BigFishScore;
            if (protoCompany.HasMarkdownNote) entity.MarkdownNote = protoCompany.MarkdownNote;

            if (protoCompany.Classification.Count > 0)
            {
                entity.Classification = protoCompany.Classification
                    .Select(c => (Modelz.CompClassification)c)
                    .Aggregate(Modelz.CompClassification.Unspecified, (acc, val) => acc | val);
            }
            else
            {
                entity.Classification = Modelz.CompClassification.Unspecified;
            }
            // entity.Classification = (Modelz.CompClassification)protoCompany.Classification; // don't use null. just use 0 unspecified for "nothing / not set"

            return entity;


        }

        public static void PatchEntityFromGrpc(ProtoApi.Company grpcCompany, IEnumerable<string> updatePaths, Modelz.Company entity)
        {

            foreach (var path in updatePaths)
            {
                switch (path)
                {
                    case "company_name":
                        entity.CompanyName = grpcCompany.HasCompanyName ? grpcCompany.CompanyName : null;
                        break;
                    case "country":
                        entity.Country = grpcCompany.HasCountry ? grpcCompany.Country : null;
                        break;
                    case "city":
                        entity.City = grpcCompany.HasCity ? grpcCompany.City : null;
                        break;
                    case "full_address":
                        entity.FullAddress = grpcCompany.HasFullAddress ? grpcCompany.FullAddress : null;
                        break;
                    case "website":
                        entity.Website = grpcCompany.HasWebsite ? grpcCompany.Website : null;
                        break;
                    case "category_google":
                        entity.CategoryGoogle = grpcCompany.HasCategoryGoogle ? grpcCompany.CategoryGoogle : null;
                        break;
                    case "rating_google":
                        entity.RatingGoogle = grpcCompany.HasRatingGoogle ? (decimal)grpcCompany.RatingGoogle : null;
                        break;
                    case "rated_count":
                        entity.RatedCount = grpcCompany.HasRatedCount ? grpcCompany.RatedCount : null;
                        break;
                    case "google_maps_url":
                        entity.GoogleMapsUrl = grpcCompany.HasGoogleMapsUrl ? grpcCompany.GoogleMapsUrl : null;
                        break;
                    case "big_fish_score":
                        entity.BigFishScore = grpcCompany.HasBigFishScore ? grpcCompany.BigFishScore : null;
                        break;
                    case "markdown_note":
                        entity.MarkdownNote = grpcCompany.HasMarkdownNote ? grpcCompany.MarkdownNote : null;
                        break;
                    case "classification":
                        entity.Classification = grpcCompany.Classification
                            .Aggregate(Modelz.CompClassification.Unspecified, (acc, next) => acc | (Modelz.CompClassification)(int)next);
                        break;
                    default:
                        throw new RpcException(new Status(StatusCode.InvalidArgument, $"Unknown update mask path: {path}"));
                }
            }

        }
    }
}