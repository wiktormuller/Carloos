namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        private const int MinimalNumberOfPage = 0;

        private const int MinimalSizeOfPage = 1;
        private const int MaximalSizeOfPage = 100;

        private readonly int _pageNumber;
        private readonly int _pageSize;
        
        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = (value < MinimalNumberOfPage) ? MinimalNumberOfPage : value;
        }

        public int PageSize
        {
            get => _pageSize;
            init
            {
                _pageSize = value switch
                {
                    < MinimalSizeOfPage => 1,
                    > MaximalSizeOfPage => MaximalSizeOfPage,
                    _ => value
                };
            }
        }
    }
}