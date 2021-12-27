using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class CreateEmploymentTypeCommandHandler : IRequestHandler<CreateEmploymentTypeCommand, int>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public CreateEmploymentTypeCommandHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = employmentTypeRepository;
        }

        public async Task<int> Handle(CreateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _employmentTypeRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var employmentType = new Domain.Entities.EmploymentType(request.Name);

            await _employmentTypeRepository.Create(employmentType);

            return employmentType.Id;
        }
    }
}