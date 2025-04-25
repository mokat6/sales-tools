namespace big_data.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string Website { get; set; }
        public string CategoryGoogle { get; set; }
        public decimal? RatingGoogle { get; set; }
        public string RatedCount { get; set; }
        public string GoogleMapsUrl { get; set; }
        public int? BigFishScore { get; set; }
        public string Class { get; set; }

        // Navigation properties for related contact details
        public ICollection<Email> Emails { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<Facebook> Facebooks { get; set; }
        public ICollection<Instagram> Instagrams { get; set; }
        public ICollection<LinkedIn> LinkedIns { get; set; }
        public ICollection<OtherContact> OtherContacts { get; set; }
    }
}
