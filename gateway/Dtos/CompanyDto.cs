namespace gatewayRoot.Dtos
{
    public class CompanyDto
    {
        // I will ignore Id in PATCH operations.
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

        // keep the DTO not the `ProtoApi.CompClassification` 
        // why? decouple. PatchCompanyDto is part of your application logic or API layer — it shouldn’t directly depend on the gRPC-generated code
        // lets me change value names, add attributes, ...
        public CompClassificationDto? Classification { get; set; }
    }
}
