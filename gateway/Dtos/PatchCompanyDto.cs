// same as CompanyDto, all fields nullable, not include ID. ID is included in URL

using ProtoApi = big_data.Proto;

namespace gatewayRoot.Dtos
{
    public class PatchCompanyDto
    {
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? FullAddress { get; set; }
        public string? Website { get; set; }
        public string? CategoryGoogle { get; set; }
        public double? RatingGoogle { get; set; }
        public string? RatedCount { get; set; }
        public string? GoogleMapsUrl { get; set; }
        public int? BigFishScore { get; set; }
        public ProtoApi.CompClassification? Classification { get; set; }
    }
}
