using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using MediatR;
using JobJetRestApi.Domain.Repositories;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateJobOfferCommandHandler(IUserRepository userRepository)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
        }

        public async Task<Unit> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            user.UpdateJobOffer(
                request.Id,
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}