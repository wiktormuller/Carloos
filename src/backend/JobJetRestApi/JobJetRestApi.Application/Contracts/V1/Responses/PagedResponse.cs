using System;
using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Ports;

namespace JobJetRestApi.Web.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public ResultMetadata Metadata { get; set; }
        public Result<T> Results { get; set; }

        public PagedResponse(T data,
            int pageNumber, 
            int pageSize, 
            int totalPages, 
            int totalRecords/*,
            Uri nextPage,
            Uri previousPage, 
            Uri firstPage,
            Uri lastPage*/)
        {
            Results = new Result<T>(data);
            Metadata = new ResultMetadata(
                pageNumber, 
                pageSize, 
                totalPages, 
                totalRecords/*,
                nextPage, 
                previousPage,
                firstPage,
                lastPage*/);
        }
    }

    public class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedResponse<T>(
            List<T> pagedData,
            PaginationFilter paginationFilter,
            int totalRecords)
        {
            var pageNumber = paginationFilter.Offset + 1;
            var pageSize = paginationFilter.Limit;
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(((double) totalRecords / (double) paginationFilter.Limit)));
            
            return new PagedResponse<List<T>>(pagedData, pageNumber, pageSize, roundedTotalPages, totalRecords);
        }
    }
}