
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;

namespace big_data.Mappers
{
    static class ContactMapper
    {

        public static Modelz.Contact GrpcToEntity(ProtoApi.AddContactRequest c)
        {
            return new Modelz.Contact()
            {
                CompanyId = c.CompanyId,
                Value = c.Value,
                Type = (Modelz.ContactType)(int)c.Type,
                ContactedFromEmail = c.ContactedFromEmail,
                Checked = c.Checked,
                Date = c.Date?.ToDateTime()

            };
        }


        public static ProtoApi.Contact EntityToGrpcFull(Modelz.Contact c)
        {
            return new ProtoApi.Contact
            {
                Id = c.Id,
                CompanyId = c.CompanyId,
                Value = c.Value,
                Type = (ProtoApi.ContactType)(int)c.Type,
                IsOnWhatsapp = c.IsOnWhatsApp,
                ContactedFromEmail = c.ContactedFromEmail,
                Checked = c.Checked,
                Date = c.Date.HasValue ? Timestamp.FromDateTime(c.Date.Value.ToUniversalTime()) : null,
            };
        }

    }
}