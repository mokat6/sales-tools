namespace big_data.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? FullAddress { get; set; }
        public string? Website { get; set; }
        public string? CategoryGoogle { get; set; }
        public decimal? RatingGoogle { get; set; }
        public string? RatedCount { get; set; }
        public string? GoogleMapsUrl { get; set; }
        public int? BigFishScore { get; set; }
        public CompClassification? Classification { get; set; }

        // Navigation properties for related contact details
        public ICollection<Contact> Contacts { get; set; } = [];


    }


}

