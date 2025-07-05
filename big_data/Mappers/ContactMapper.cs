
using ProtoApi = big_data.Proto;
using Modelz = big_data.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.StaticAssets;

namespace big_data.Mappers
{
    static class ContactMapper
    {

        public static Modelz.Contact GrpcToEntity(ProtoApi.AddContactRequest grpc)
        {
            Modelz.Contact entity = new()
            {
                Value = grpc.Value,
            };
            entity.CompanyId = grpc.CompanyId;
            entity.Type = (Modelz.ContactType)grpc.Type;

            if (grpc.HasContactedFromEmail) entity.ContactedFromEmail = grpc.ContactedFromEmail;
            if (grpc.HasChecked) entity.Checked = grpc.Checked;
            if (grpc.Date != null) entity.Date = grpc.Date.ToDateTime();

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

        // TODO: check the put Update Contact, will need FIXING
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