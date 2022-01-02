﻿using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IJobOfferRepository
    {
        Task<JobOffer> GetById(int id);
        Task<bool> Exists(int id);
        Task<int> Create(JobOffer jobOffer);
        Task Update();
        Task Delete(int id);
    }
}