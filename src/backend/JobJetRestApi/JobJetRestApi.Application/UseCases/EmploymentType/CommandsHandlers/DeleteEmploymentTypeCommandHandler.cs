using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
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
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
        }
        
        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        public async Task<Unit> Handle(DeleteEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await _employmentTypeRepository.Exists(request.Id))
            {
                throw EmploymentTypeNotFoundException.ForId(request.Id);
            }

            var employmentType = await _employmentTypeRepository.GetById(request.Id);

            await _employmentTypeRepository.Delete(employmentType);
            
            return Unit.Value;
        }
    }
}