namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        private const int MinimalPageNumber = 0;
        private const int MaximalPageSize = 100;

        private readonly int _pageNumber;
        private readonly int _pageSize;
        
        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = (value < MinimalPageNumber) ? MinimalPageNumber : value;
        }

        public int PageSize //@TODO -  It's always 0 what is invalid in sql
        {
            get => _pageSize;
            init => _pageSize = (value > MaximalPageSize) ? MaximalPageSize : value;
        }
    }
}