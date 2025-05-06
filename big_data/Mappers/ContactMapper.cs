
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.StaticAssets;

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

        public static void UpdateContact(ProtoApi.UpdateContactRequest request, Modelz.Contact entity)
        {
            if (request.HasCompanyId) entity.CompanyId = request.CompanyId;
            if (request.HasValue) entity.Value = request.Value;
            if (request.HasType) entity.Type = (Modelz.ContactType)request.Type;
            if (request.IsOnWhatsapp != null) entity.IsOnWhatsApp = request.IsOnWhatsapp;
            if (request.HasContactedFromEmail) entity.ContactedFromEmail = request.ContactedFromEmail;
            if (request.Checked != null) entity.Checked = request.Checked;
            if (request.Date != null) entity.Date = request.Date.ToDateTime();
        }

    }
}