using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using JobJetRestApi.Domain.Repositories;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.CommandsHandlers
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
            _companyRepository = Guard.Against.Null(companyRepository, nameof(companyRepository));
        }
        
        /// <exception cref="CompanyNotFoundException"></exception>
        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!await _companyRepository.ExistsAsync(request.Id))
            {
                throw CompanyNotFoundException.ForId(request.Id);
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);
            user.DeleteCompany(request.Id); // @TODO - There is orphan after this action

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}