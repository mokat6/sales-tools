
using gatewayRoot.Dtos;
using ProtoApi = big_data.Proto;

namespace gatewayRoot.Mappers;

static class CompClassificationMapper
{
    public static string? ToLabel(ProtoApi.CompClassification grpcEnum)
    {
        return grpcEnum switch
        {
            ProtoApi.CompClassification.Unspecified => null,
            ProtoApi.CompClassification.GoodMatch => "GoodMatch",
            ProtoApi.CompClassification.FuckYou => "FuckYou",
            ProtoApi.CompClassification.Ecommerce => "Ecommerce",
            ProtoApi.CompClassification.GimmeSomeLove => "GimmeSomeLove",
            _ => null,
        };
    }

}
