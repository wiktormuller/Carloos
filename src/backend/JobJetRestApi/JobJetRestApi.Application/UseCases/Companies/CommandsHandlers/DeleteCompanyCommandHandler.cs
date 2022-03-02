using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using MediatR;
using JobJetRestApi.Application.Repositories;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.ExistsAsync(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            var company = await _companyRepository.GetByIdAsync(request.Id);
            await _companyRepository.DeleteAsync(company);

            return Unit.Value;
        }
    }
}