namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        private const int MinimalPageNumber = 1;
        private const int MaximalPageSize = 100;
        
        public int PageNumber { get; }
        public int PageSize { get; }
        
        public string OrderBy { get; set; } // To implement

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