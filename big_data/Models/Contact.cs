namespace big_data.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public Company? Company { get; set; }

        public required string Value { get; set; }  // The contact value (e.g., email address, phone number, etc.)

        public ContactType Type { get; set; } // Enum to distinguish between types of contact
        public bool? IsOnWhatsApp { get; set; }  // For phone numbers only (optional)
        public string? ContactedFromEmail { get; set; }  // For emails only (optional)
        public bool? Checked { get; set; }
        public DateTime? Date { get; set; }
    }

    public enum ContactType
    {
        Email,
        PhoneNumber,
        Facebook,
        Instagram,
        LinkedIn,
        Other
    }
}
