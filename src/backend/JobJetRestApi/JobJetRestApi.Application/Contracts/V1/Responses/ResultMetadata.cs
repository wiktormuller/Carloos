using System;

namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class ResultMetadata
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        
        public int TotalPages { get; }
        public int TotalRecords { get; }

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
        
        public Uri NextPage { get; }
        public Uri PreviousPage { get; }
        
        public Uri FirstPage { get; }
        public Uri LastPage { get; }

        public ResultMetadata(
            int pageNumber, 
            int pageSize,
            int totalPages,
            int totalRecords, 
            Uri nextPage,
            Uri previousPage, 
            Uri firstPage, 
            Uri lastPage)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            NextPage = nextPage;
            PreviousPage = previousPage;
            FirstPage = firstPage;
            LastPage = lastPage;
        }
    }
}