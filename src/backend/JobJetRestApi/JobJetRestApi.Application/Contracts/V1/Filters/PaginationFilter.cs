namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter // @TODO - Should it be abstract?
    {
        private const int MinimalPageNumber = 0;
        private const int MaximalPageSize = 100;
        
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginationFilter() : this(MinimalPageNumber, MaximalPageSize)
        {
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = (pageNumber < MinimalPageNumber) ? MinimalPageNumber : pageNumber;
            PageSize = (pageSize > MaximalPageSize) ? MaximalPageSize : pageSize;
        }
    }
}