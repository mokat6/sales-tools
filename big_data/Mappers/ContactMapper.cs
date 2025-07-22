
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.StaticAssets;

namespace big_data.Mappers
{
    static class ContactMapper
    {

        public static Modelz.Contact AddContactGrpcToEntity(ProtoApi.AddContactRequest grpc)
        {
            Modelz.Contact entity = new()
            {
                Value = grpc.Value,
            };
            entity.CompanyId = grpc.CompanyId;
            entity.Type = (Modelz.ContactType)grpc.Type;



            return entity;
        }


        public static ProtoApi.Contact EntityToGrpcFull(Modelz.Contact entity)
        {
            var grpc = new ProtoApi.Contact();

            grpc.Id = entity.Id;
            grpc.CompanyId = entity.CompanyId;
            grpc.Value = entity.Value;
            grpc.Type = (ProtoApi.ContactType)entity.Type;

            if (entity.IsOnWhatsapp != null) grpc.IsOnWhatsapp = entity.IsOnWhatsapp.Value;
            if (entity.ContactedFromEmail != null) grpc.ContactedFromEmail = entity.ContactedFromEmail;
            if (entity.Checked != null) grpc.Checked = entity.Checked.Value;
            if (entity.Date.HasValue) grpc.Date = Timestamp.FromDateTime(entity.Date.Value.ToUniversalTime());

            return grpc;
        }

        // PUT - if a field is not set, it will be nulled out
        public static void PutUpdateContact(ProtoApi.UpdateContactRequest grpc, Modelz.Contact entity)
        {
            entity.CompanyId = grpc.CompanyId;
            entity.Value = grpc.Value;
            entity.Type = (Modelz.ContactType)grpc.Type;
            entity.IsOnWhatsapp = grpc.HasIsOnWhatsapp ? grpc.IsOnWhatsapp : null;
            entity.ContactedFromEmail = grpc.HasContactedFromEmail ? grpc.ContactedFromEmail : null;
            entity.Checked = grpc.HasChecked ? grpc.Checked : null;

            entity.Date = grpc.Date?.ToDateTime();
        }

    }
}