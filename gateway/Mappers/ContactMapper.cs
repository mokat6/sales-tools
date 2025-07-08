
using gatewayRoot.Dtos;
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

}
