
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
            var grpc = new ProtoApi.Contact();

            grpc.Id = c.Id;
            grpc.CompanyId = c.CompanyId;
            if (c.Value != null) grpc.Value = c.Value;
            grpc.Type = (ProtoApi.ContactType)c.Type;
            if (c.IsOnWhatsapp != null) grpc.IsOnWhatsapp = c.IsOnWhatsapp;
            if (c.ContactedFromEmail != null) grpc.ContactedFromEmail = c.ContactedFromEmail;
            if (c.Checked != null) grpc.Checked = c.Checked;
            if (c.Date.HasValue) grpc.Date = Timestamp.FromDateTime(c.Date.Value.ToUniversalTime());

            return grpc;
        }

        public static void UpdateContact(ProtoApi.UpdateContactRequest request, Modelz.Contact entity)
        {
            if (request.HasCompanyId) entity.CompanyId = request.CompanyId;
            if (request.HasValue) entity.Value = request.Value;
            if (request.HasType) entity.Type = (Modelz.ContactType)request.Type;
            if (request.IsOnWhatsapp != null) entity.IsOnWhatsapp = request.IsOnWhatsapp;
            if (request.HasContactedFromEmail) entity.ContactedFromEmail = request.ContactedFromEmail;
            if (request.Checked != null) entity.Checked = request.Checked;
            if (request.Date != null) entity.Date = request.Date.ToDateTime();
        }

    }
}