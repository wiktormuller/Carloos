﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.TechnologyType.CommandsHandlers
{
    public class DeleteTechnologyTypeCommandHandler : IRequestHandler<DeleteTechnologyTypeCommand>
    {
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        
        public DeleteTechnologyTypeCommandHandler(ITechnologyTypeRepository technologyTypeRepository)
        {
            _technologyTypeRepository = technologyTypeRepository;
        }
        
        public async Task<Unit> Handle(DeleteTechnologyTypeCommand request, CancellationToken cancellationToken)
        {
            if (! await _technologyTypeRepository.Exists(request.Id))
            {
                throw new ArgumentException(nameof(request.Id));
                // @TODO - Throw Domain Exception
            }

            var technologyType = await _technologyTypeRepository.GetById(request.Id);

            await _technologyTypeRepository.Delete(technologyType);
            
            return Unit.Value;
        }
    }
}