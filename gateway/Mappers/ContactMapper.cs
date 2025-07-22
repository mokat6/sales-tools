
using gatewayRoot.Dtos;
using Google.Protobuf.WellKnownTypes;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class ContactMapper
{
    public static ContactDto GrpcToDto(ProtoApi.Contact grpc)
    {
        ContactDto dto = new()
        {
            Id = grpc.Id,
            Value = grpc.Value
        };
        dto.Id = grpc.Id;
        dto.CompanyId = grpc.CompanyId;
        dto.Type = (ContactTypeDto)grpc.Type;

        if (grpc.HasChecked) dto.Checked = grpc.Checked;
        if (grpc.HasContactedFromEmail) dto.ContactedFromEmail = grpc.ContactedFromEmail;
        if (grpc.HasIsOnWhatsapp) dto.IsOnWhatsapp = grpc.IsOnWhatsapp;
        if (grpc.Date != null) dto.Date = grpc.Date.ToDateTime();

        return dto;
    }

    public static ProtoApi.AddContactRequest createRequestDtoToGrpc(CreateContactDto dto)
    {
        ProtoApi.AddContactRequest grpc = new();
        grpc.CompanyId = dto.CompanyId;
        if (dto.Value != null) grpc.Value = dto.Value;
        grpc.Type = (ProtoApi.ContactType)dto.Type;

        return grpc;
    }

    // PUT - so replace whole object, gRPC can't have nulls. so just not set. On gRPC server, if not set then map to null
    public static ProtoApi.UpdateContactRequest updateRequestDtoToGrpc(ContactDto dto)
    {
        ProtoApi.UpdateContactRequest grpc = new();
        grpc.Id = dto.Id;
        grpc.CompanyId = dto.CompanyId;
        grpc.Type = (ProtoApi.ContactType)dto.Type;
        if (dto.Value != null) grpc.Value = dto.Value;
        if (dto.Checked.HasValue) grpc.Checked = dto.Checked.Value;
        if (dto.ContactedFromEmail != null) grpc.ContactedFromEmail = dto.ContactedFromEmail;
        if (dto.Date.HasValue)
        {
            grpc.Date = Timestamp.FromDateTime(dto.Date.Value.ToUniversalTime());
        }
        if (dto.IsOnWhatsapp.HasValue) grpc.IsOnWhatsapp = dto.IsOnWhatsapp.Value;

        return grpc;
    }
}
