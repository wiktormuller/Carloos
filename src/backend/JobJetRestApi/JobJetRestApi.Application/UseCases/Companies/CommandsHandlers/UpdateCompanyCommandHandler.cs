using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        /// <exception cref="CompanyAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.ExistsAsync(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            if (await _companyRepository.ExistsAsync(request.Name))
            {
                throw CompanyAlreadyExistsException.ForName(request.Name);
            }

            var company = await _companyRepository.GetByIdAsync(request.Id);
            company.Update(request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);

            await _companyRepository.UpdateAsync();

            return Unit.Value;
        }
    }
}