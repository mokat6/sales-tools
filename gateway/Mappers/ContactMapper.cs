
using gatewayRoot.Dtos;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class ContactMapper
{
    public static ContactDto GrpcToDto(ProtoApi.Contact c)
    {
        return new ContactDto
        {
            Id = c.Id,
            CompanyId = c.CompanyId,
            Value = c.Value,
            Type = c.Type,
            IsOnWhatsapp = c.IsOnWhatsapp,
            ContactedFromEmail = c.ContactedFromEmail,
            Checked = c.Checked,
            Date = c.Date?.ToDateTime(),

        };
    }

}
