using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;
using JobJetRestApi.Application.Repositories;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }
        
        /// <exception cref="CompanyAlreadyExistsException"></exception>
        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _companyRepository.ExistsAsync(request.Name))
            {
                throw CompanyAlreadyExistsException.ForName(request.Name);
            }

            var company = new Company(request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);
            await _companyRepository.CreateAsync(company);

            return company.Id;
        }
    }
}