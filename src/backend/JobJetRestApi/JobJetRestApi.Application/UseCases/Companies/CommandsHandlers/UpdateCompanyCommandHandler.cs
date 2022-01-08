using System;
using System.Threading;
using System.Threading.Tasks;
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
        
        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _companyRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var company = await _companyRepository.GetById(request.Id);
            company.Update(request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);

            await _companyRepository.Update();

            return Unit.Value;
        }
    }
}