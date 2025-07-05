using ProtoApi = big_data.Proto;

namespace gatewayRoot.Dtos
{
    public class ContactDto
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public required string Value { get; set; }
        public ContactTypeDto Type { get; set; }
        public bool? IsOnWhatsapp { get; set; }
        public string? ContactedFromEmail { get; set; }
        public bool? Checked { get; set; }
        public DateTime? Date { get; set; }

    }

    public enum ContactTypeDto
    {
        Unspecified = 0,
        Email = 1,
        PhoneNumber = 2,
        Facebook = 3,
        Instagram = 4,
        LinkedIn = 5,
        Other = 6
    }
}


