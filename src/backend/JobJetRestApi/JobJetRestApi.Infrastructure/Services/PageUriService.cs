using System;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Ports;

namespace JobJetRestApi.Infrastructure.Services
{
    public class PageUriService : IPageUriService
    {
        public Uri GetPageUri(PaginationFilter paginationFilter, string route)
        {
            throw new NotImplementedException();
        }
    }
}