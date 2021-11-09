using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Persistence.Seeders
{
    public class JobOfferAggregateSeeder
    {
        public static async Task SeedAsync(JobJetDbContext context, int retryCounter = 0)
        {
            var retryForAvailability = retryCounter;
            
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

            // THINK ABOUT THE ORDER! Use logger here or in the program?
            try
            {
                if (!await context.JobOffers.AnyAsync())
                {
                    await context.JobOffers.AddRangeAsync(
                        GetPredefinedJobOffers());
                    await context.SaveChangesAsync();
                }
            
                if (!await context.SeniorityLevels.AnyAsync())
                {
                    await context.SeniorityLevels.AddRangeAsync(
                        GetPredefinedSeniorityLevels());
                    await context.SaveChangesAsync();
                }

                if (!await context.TechnologyTypes.AnyAsync())
                {
                    await context.TechnologyTypes.AddRangeAsync(
                        GetPredefinedTechnologyTypes());
                    await context.SaveChangesAsync();
                }

                if (!await context.EmploymentTypes.AnyAsync())
                {
                    await context.EmploymentTypes.AddRangeAsync(
                        GetPredefinedEmploymentTypes());
                    await context.SaveChangesAsync();
                }

                if (!await context.Countries.AnyAsync())
                {
                    await context.Countries.AddRangeAsync(
                        GetPredefinedCountries());
                    await context.SaveChangesAsync();
                }

                if (!await context.Addresses.AnyAsync())
                {
                    await context.Addresses.AddRangeAsync(
                        GetPredefinedAddresses());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                if (retryForAvailability >= 10) throw;
                retryForAvailability++;
                await SeedAsync(context, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Address> GetPredefinedAddresses()
        {
            return new List<Address>();
        }

        private static IEnumerable<Country> GetPredefinedCountries()
        {
            return new List<Country>();
        }

        private static IEnumerable<EmploymentType> GetPredefinedEmploymentTypes()
        {
            return new List<EmploymentType>();
        }

        private static IEnumerable<TechnologyType> GetPredefinedTechnologyTypes()
        {
            return new List<TechnologyType>();
        }

        private static IEnumerable<Seniority> GetPredefinedSeniorityLevels()
        {
            return new List<Seniority>();
        }

        private static IEnumerable<JobOffer> GetPredefinedJobOffers()
        {
            return new List<JobOffer>();
        }
    }
}