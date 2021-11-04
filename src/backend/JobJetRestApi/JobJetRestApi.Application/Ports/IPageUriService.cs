using System;
using JobJetRestApi.Application.Contracts.V1.Filters;

namespace JobJetRestApi.Application.Ports
{
    public interface IPageUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}