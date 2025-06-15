using gatewayRoot.Dtos;
namespace gatewayRoot.Dtos
{
    public class CompaniesResponseCursor
    {
        public IEnumerable<CompanyDto> Companies { get; set; } = [];
        public PaginationCursorDto Pagination { get; set; } = new(0, null);
    }

    public record PaginationCursorDto(int TotalCount, string? NextCursor);

}
