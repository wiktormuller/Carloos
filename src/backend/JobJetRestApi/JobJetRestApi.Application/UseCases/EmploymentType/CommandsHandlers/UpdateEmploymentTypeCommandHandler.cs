using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
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
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
        }
        
        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        /// <exception cref="EmploymentTypeAlreadyExistsException"></exception>
        public async Task<Unit> Handle(UpdateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await _employmentTypeRepository.Exists(request.Id))
            {
                throw EmploymentTypeNotFoundException.ForId(request.Id);
            }

            if (await _employmentTypeRepository.Exists(request.Name))
            {
                throw EmploymentTypeAlreadyExistsException.ForName(request.Name);
            }

            var employmentType = await _employmentTypeRepository.GetById(request.Id);
            employmentType.UpdateName(request.Name);
            
            return Unit.Value;
        }
    }
}