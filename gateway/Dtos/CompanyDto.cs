using ProtoApi = big_data.Proto;

namespace gatewayRoot.Dtos
{
    public class CompanyDto
    {
        public long Id { get; set; }
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
        public string? Classification { get; set; }
    }
}
