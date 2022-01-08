using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using MediatR;
using System.Linq;

namespace JobJetRestApi.Application.UseCases.Companies.QueriesHandlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyResponse>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<CompanyResponse>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAll();
            
            return companies
                .Select(x => new CompanyResponse(x.Id, x.Name, x.ShortName, x.Description, x.NumberOfPeople, x.CityName))
                .ToList();
        }
    }
}