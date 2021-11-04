namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        //private const int MinimalOffset = 0;
        //private const int MaximumLimit = 100;

        private const int MinimalPageNumber = 1;
        private const int MaximalPageSize = 100;
        
        public int PageNumber { get; }
        public int PageSize { get; }

        public PaginationFilter()
        {
            PageNumber = MinimalPageNumber;
            PageSize = MaximalPageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = (pageNumber < MinimalPageNumber) ? MinimalPageNumber : pageNumber;
            PageSize = (pageSize > MaximalPageSize) ? MaximalPageSize : pageSize;
        }
    }
}