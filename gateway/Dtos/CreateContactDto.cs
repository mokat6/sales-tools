
namespace gatewayRoot.Dtos
{
    public class CreateContactDto
    {
        public long CompanyId { get; set; }
        public required string Value { get; set; }
        public ContactTypeDto Type { get; set; }
        // public bool? IsOnWhatsapp { get; set; }
        // public string? ContactedFromEmail { get; set; }
        // public bool? Checked { get; set; }

    }

}


