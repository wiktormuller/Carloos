using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.QueriesHandlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        public async Task<CompanyResponse> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.Exists(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            var company = await _companyRepository.GetById(request.Id);

            var result = new CompanyResponse(company.Id, company.Name, company.ShortName, company.Description, company.NumberOfPeople, company.CityName);

            return result;
        }
    }
}