using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        
        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        /// <exception cref="CompanyAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.Exists(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            if (await _companyRepository.Exists(request.Name))
            {
                throw CompanyAlreadyExistsException.ForName(request.Name);
            }

            var company = await _companyRepository.GetById(request.Id);
            company.Update(request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);

            await _companyRepository.Update();

            return Unit.Value;
        }
    }
}