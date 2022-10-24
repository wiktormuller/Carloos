namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; init; }

        public int PageSize { get; set; } = 10;

        public int GetNormalizedPageSize()
        {
            return PageSize + 1;
        }
    }
}