using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class UpdateEmploymentTypeCommandHandler : IRequestHandler<UpdateEmploymentTypeCommand>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public UpdateEmploymentTypeCommandHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = employmentTypeRepository;
        }
        
        public async Task<Unit> Handle(UpdateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await _employmentTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            if (await _employmentTypeRepository.Exists(request.Name))
            {
                throw new ArgumentException(nameof(request.Name));
                // @TODO - Throw Domain Exception
            }

            var employmentType = await _employmentTypeRepository.GetById(request.Id);
            employmentType.UpdateName(request.Name);
            
            return Unit.Value;
        }
    }
}