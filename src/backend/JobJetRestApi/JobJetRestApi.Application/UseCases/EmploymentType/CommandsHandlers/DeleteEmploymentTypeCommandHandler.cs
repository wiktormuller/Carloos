using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class DeleteEmploymentTypeCommandHandler : IRequestHandler<DeleteEmploymentTypeCommand>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public DeleteEmploymentTypeCommandHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = employmentTypeRepository;
        }

        public async Task<Unit> Handle(DeleteEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await _employmentTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var employmentType = await _employmentTypeRepository.GetById(request.Id);

            await _employmentTypeRepository.Delete(employmentType);
            
            return Unit.Value;
        }
    }
}