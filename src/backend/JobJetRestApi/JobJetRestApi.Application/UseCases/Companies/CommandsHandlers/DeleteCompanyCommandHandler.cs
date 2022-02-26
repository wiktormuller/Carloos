using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.Exists(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            var company = await _companyRepository.GetById(request.Id);
            await _companyRepository.Delete(company);

            return Unit.Value;
        }
    }
}