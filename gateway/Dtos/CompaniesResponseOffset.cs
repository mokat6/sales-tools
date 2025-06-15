
namespace gatewayRoot.Dtos
{

    public class CompaniesResponseOffset
    {
        public IEnumerable<CompanyDto> Companies { get; set; } = [];
        public PaginationDto Pagination { get; set; } = new();
    }

    public class PaginationDto
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}