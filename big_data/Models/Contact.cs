namespace big_data.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Company? Company { get; set; }

        public required string Value { get; set; }  // The contact value (e.g., email address, phone number, etc.)

        public ContactType Type { get; set; } // Enum to distinguish between types of contact
        public bool? IsOnWhatsapp { get; set; }  // For phone numbers only (optional)
        public string? ContactedFromEmail { get; set; }  // For emails only (optional)
        public bool? Checked { get; set; }
        public DateTime? Date { get; set; }
    }

    public enum ContactType
    {
        // default, should never be used, because ContactType is not nullable
        // can throw error if ContactType = 0. Like... if it is required
        Unspecified = 0,
        Email = 1,
        PhoneNumber = 2,
        Facebook = 3,
        Instagram = 4,
        LinkedIn = 5,
        Other = 6
    }
}
