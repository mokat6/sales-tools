
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


        public static ProtoApi.Contact EntityToGrpcFull(Modelz.Contact entity)
        {
            var grpc = new ProtoApi.Contact();

            grpc.Id = entity.Id;
            grpc.CompanyId = entity.CompanyId;
            if (entity.Value != null) grpc.Value = entity.Value;
            grpc.Type = (ProtoApi.ContactType)entity.Type;

            if (entity.IsOnWhatsapp != null) grpc.IsOnWhatsapp = entity.IsOnWhatsapp;
            if (entity.ContactedFromEmail != null) grpc.ContactedFromEmail = entity.ContactedFromEmail;
            if (entity.Checked != null) grpc.Checked = entity.Checked;
            if (entity.Date.HasValue) grpc.Date = Timestamp.FromDateTime(entity.Date.Value.ToUniversalTime());

            return grpc;
        }

        public static void PutUpdateContact(ProtoApi.UpdateContactRequest request, Modelz.Contact entity)
        {
            entity.CompanyId = request.CompanyId;
            entity.Value = request.Value;
            entity.Type = (Modelz.ContactType)request.Type;
            entity.IsOnWhatsapp = request.HasIsOnWhatsapp ? request.IsOnWhatsapp : null;
            entity.ContactedFromEmail = request.HasContactedFromEmail ? request.ContactedFromEmail : null;
            entity.Checked = request.HasChecked ? request.Checked : null;
            entity.Date = request.Date.ToDateTime();
        }

    }
}