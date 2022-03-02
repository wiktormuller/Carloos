using System;
using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Ports;

namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class PagedResponse<T>
    {
        public Response<T> Response { get; }
        public ResultMetadata Metadata { get; }

        private PagedResponse(T data,
            string message,
            bool succeeded,
            string[] errors,
            int pageNumber, 
            int pageSize, 
            int totalPages, 
            int totalRecords,
            Uri nextPage,
            Uri previousPage, 
            Uri firstPage,
            Uri lastPage)
        {
            Response = new Response<T>(
                data, 
                message, 
                succeeded, 
                errors);
            
            Metadata = new ResultMetadata(
                pageNumber, 
                pageSize, 
                totalPages, 
                totalRecords,
                nextPage, 
                previousPage,
                firstPage,
                lastPage);
        }
        
        public static PagedResponse<List<T>> CreatePagedResponse<T>(
            List<T> pagedData,
            string message,
            bool succeeded,
            string[] errors,
            PaginationFilter paginationFilter,
            int totalRecords,
            IPageUriService pageUriService,
            string route)
        {
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(((double) totalRecords / (double) paginationFilter.PageSize)));

            var nextPage = paginationFilter.PageNumber >= 1 && paginationFilter.PageNumber < roundedTotalPages
                ? pageUriService.GetPageUri(new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize), route)
                : null;
            
            var previousPage = paginationFilter.PageNumber - 1 >= 1 && paginationFilter.PageNumber <= roundedTotalPages
                ? pageUriService.GetPageUri(new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize), route)
                : null;
            
            var firstPage = pageUriService.GetPageUri(new PaginationFilter(1, paginationFilter.PageSize), route);
            var lastPage = pageUriService.GetPageUri(new PaginationFilter(roundedTotalPages, paginationFilter.PageSize), route);
            
            return new PagedResponse<List<T>>(
                pagedData,
                message,
                succeeded,
                errors,
                paginationFilter.PageNumber, 
                paginationFilter.PageSize, 
                roundedTotalPages, 
                totalRecords, 
                nextPage, 
                previousPage, 
                firstPage, 
                lastPage);
        }
    }
}