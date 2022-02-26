﻿using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Entities;
using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.CommandsHandlers
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, int>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly ISeniorityRepository _seniorityRepository;
        private readonly ITechnologyTypeRepository _technologyTypeRepository;
        private readonly IEmploymentTypeRepository _employmentTypeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly ICurrencyRepository _currencyRepository;

        public CreateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, 
            ISeniorityRepository seniorityRepository, 
            ITechnologyTypeRepository technologyTypeRepository, 
            IEmploymentTypeRepository employmentTypeRepository, 
            ICountryRepository countryRepository, 
            IGeocodingService geocodingService, 
            ICurrencyRepository currencyRepository)
        {
            _jobOfferRepository = jobOfferRepository;
            _seniorityRepository = seniorityRepository;
            _technologyTypeRepository = technologyTypeRepository;
            _employmentTypeRepository = employmentTypeRepository;
            _countryRepository = countryRepository;
            _geocodingService = geocodingService;
            _currencyRepository = currencyRepository;
        }

        /// <exception cref="SeniorityLevelNotFoundException"></exception>
        /// <exception cref="TechnologyTypeNotFoundException"></exception>
        /// <exception cref="EmploymentTypeNotFoundException"></exception>
        /// <exception cref="CountryNotFoundException"></exception>
        /// <exception cref="CurrencyNotFoundException"></exception>
        /// <exception cref="InvalidAddressException"></exception>
        public async Task<int> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            if (! await _seniorityRepository.Exists(request.SeniorityId))
            {
                throw SeniorityLevelNotFoundException.ForId(request.SeniorityId);
            }

            if (! await _technologyTypeRepository.Exists(request.TechnologyTypeId))
            {
                throw TechnologyTypeNotFoundException.ForId(request.TechnologyTypeId);
            }

            if (! await _employmentTypeRepository.Exists(request.EmploymentTypeId))
            {
                throw EmploymentTypeNotFoundException.ForId(request.EmploymentTypeId);
            }

            if (! await _countryRepository.Exists(request.CountryId))
            {
                throw CountryNotFoundException.ForId(request.CountryId);
            }

            if (! await _currencyRepository.Exists(request.CurrencyId))
            {
                throw CurrencyNotFoundException.ForId(request.CurrencyId);
            }

            var addressFromInput = request.Street + " " + request.ZipCode + " " + request.Town;

            var addressCoords = await _geocodingService.ConvertAddressIntoCoords(addressFromInput);
            if (addressCoords is null)
            {
                throw InvalidAddressException.Default(addressFromInput);
            }
            
            var country = await _countryRepository.GetById(request.CountryId);
            var address = new Address(country, request.Town, request.Street, request.ZipCode, addressCoords.Latitude, addressCoords.Longitude);

            var technologyType = await _technologyTypeRepository.GetById(request.TechnologyTypeId);

            var seniorityLevel = await _seniorityRepository.GetById(request.SeniorityId);

            var employmentType = await _employmentTypeRepository.GetById(request.EmploymentTypeId);

            var currency = await _currencyRepository.GetById(request.CurrencyId);
            
            var jobOffer = new JobOffer(
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo,
                address,
                technologyType,
                seniorityLevel,
                employmentType,
                currency
                );

            var jobOfferId = await _jobOfferRepository.Create(jobOffer);

            return jobOfferId;
        }
    }
}