namespace JobJetRestApi.Application.Contracts.V1.Filters
{
    public class PaginationFilter
    {
        private const int MinimalOffset = 0;
        private const int MaximumLimit = 100;
        
        public int Offset { get; set; }
        public int Limit { get; set; }

        public PaginationFilter()
        {
            Offset = MinimalOffset;
            Limit = MaximumLimit;
        }

        public PaginationFilter(int offset, int limit)
        {
            Offset = (offset < MinimalOffset) ? MinimalOffset : offset;
            Limit = (limit > MaximumLimit) ? MaximumLimit : limit;
        }
    }
}