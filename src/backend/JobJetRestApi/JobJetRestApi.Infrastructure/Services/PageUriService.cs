using System;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Ports;
using Microsoft.AspNetCore.WebUtilities;

namespace JobJetRestApi.Infrastructure.Services
{
    public class PageUriService : IPageUriService
    {
        private readonly string _baseUri;
        
        public PageUriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(_baseUri + route);
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            
            return new Uri(modifiedUri);
        }
    }
}