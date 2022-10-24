using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.EmploymentType.CommandsHandlers
{
    public class CreateEmploymentTypeCommandHandler : IRequestHandler<CreateEmploymentTypeCommand, int>
    {
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        
        public CreateEmploymentTypeCommandHandler(IEmploymentTypeRepository employmentTypeRepository)
        {
            _employmentTypeRepository = Guard.Against.Null(employmentTypeRepository, nameof(employmentTypeRepository));
        }

        /// <exception cref="EmploymentTypeAlreadyExistsException"></exception>
        public async Task<int> Handle(CreateEmploymentTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _employmentTypeRepository.ExistsAsync(request.Name))
            {
                throw EmploymentTypeAlreadyExistsException.ForName(request.Name);
            }

            var employmentType = new Domain.Entities.EmploymentType(request.Name);

            await _employmentTypeRepository.CreateAsync(employmentType);

            return employmentType.Id;
        }
    }
}