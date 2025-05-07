using ProtoApi = big_data.Proto;

namespace gatewayRoot.Dtos
{
    public class ContactDto
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public required string Value { get; set; }
        public ProtoApi.ContactType? Type { get; set; }

        public bool? IsOnWhatsapp { get; set; }
        public string? ContactedFromEmail { get; set; }
        public bool? Checked { get; set; }
        public DateTime? Date { get; set; }

    }
}


