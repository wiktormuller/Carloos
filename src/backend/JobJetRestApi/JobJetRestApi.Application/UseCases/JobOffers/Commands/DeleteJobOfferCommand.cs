using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.Commands
{
    public class DeleteJobOfferCommand : IRequest
    {
        public int CurrentUserId { get; }
        public int Id { get; }

        public DeleteJobOfferCommand(int currentUserId, int id)
        {
            CurrentUserId = currentUserId;
            Id = id;
        }
    }
}