using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Persistence.Seeders
{
    public class JobJetContextSeeder
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
                if (!await context.Currencies.AnyAsync())
                {
                    await context.Currencies.AddRangeAsync(
                        GetPredefinedCurrencies());
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

                if (!await context.Companies.AnyAsync())
                {
                    await context.Companies.AddRangeAsync(
                        GetPredefinedCompanies());
                    await context.SaveChangesAsync();
                }

                /*
                if (!await context.Addresses.AnyAsync())
                {
                    await context.Addresses.AddRangeAsync(
                        GetPredefinedAddresses());
                    await context.SaveChangesAsync();
                }
                */
                
                /*
                if (!await context.JobOffers.AnyAsync())
                {
                    await context.JobOffers.AddRangeAsync(
                        GetPredefinedJobOffers());
                    await context.SaveChangesAsync();
                }
                */
                
                //@TODO - Add user data seed
            }
            catch (Exception e)
            {
                if (retryForAvailability >= 10) throw;
                retryForAvailability++;
                await SeedAsync(context, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Currency> GetPredefinedCurrencies()
        {
            return new List<Currency>
            {
                new Currency("Polish złoty", "PLN", 985),
                new Currency("Euro", "EUR", 978),
                new Currency("United States dollar", "USD", 840),
                new Currency("Pound sterling", "GBP", 826),
                new Currency("Swiss franc", "CHF", 756)
            };
        }

        private static IEnumerable<Country> GetPredefinedCountries()
        {
            return new List<Country>
            {
                new Country("Poland", "PL", "POL", 616),
                new Country("United Kingdom", "GB", "GBR", 826),
                new Country("Germany", "DE", "DEU", 276),
                new Country("Switzerland", "CH", "CHE", 756),
                new Country("Belgium", "BE", "BEL", 056)
            };
        }

        private static IEnumerable<EmploymentType> GetPredefinedEmploymentTypes()
        {
            return new List<EmploymentType>
            {
                new EmploymentType("B2B"),
                new EmploymentType("Permanent"),
                new EmploymentType("Mandate Contract")
            };
        }

        private static IEnumerable<TechnologyType> GetPredefinedTechnologyTypes()
        {
            return new List<TechnologyType>
            {
                new TechnologyType("All"),
                new TechnologyType("JS"),
                new TechnologyType("HTML"),
                new TechnologyType("PHP"),
                new TechnologyType("Ruby"),
                new TechnologyType("Python"),
                new TechnologyType("Java"),
                new TechnologyType(".NET"),
                new TechnologyType("Scala"),
                new TechnologyType("C"),
                new TechnologyType("Mobile"),
                new TechnologyType("Testing"),
                new TechnologyType("DevOps"),
                new TechnologyType("DevOps"),
                new TechnologyType("Admin"),
                new TechnologyType("UX/UI"),
                new TechnologyType("PM"),
                new TechnologyType("Game"),
                new TechnologyType("Analytics"),
                new TechnologyType("Security"),
                new TechnologyType("Data"),
                new TechnologyType("Go"),
                new TechnologyType("Support"),
                new TechnologyType("ERP"),
                new TechnologyType("Architecture"),
                new TechnologyType("Other")
            };
        }

        private static IEnumerable<Seniority> GetPredefinedSeniorityLevels()
        {
            return new List<Seniority>
            {
                new Seniority("Intern"),
                new Seniority("Junior"),
                new Seniority("Mid"),
                new Seniority("Senior")
            };
        }

        private static IEnumerable<Company> GetPredefinedCompanies()
        {
            return new List<Company>
            {
                new Company("Apple Computer Company", "Apple", 
                    "Apple Inc. is an American multinational technology company that specializes in consumer " +
                    "electronics, computer software and online services. Apple is the largest information technology company by revenue",
                    147000, "Cupertino, California, United States"),
                new Company("Advanced Micro Devices", "AMD",
                    "Advanced Micro Devices, Inc. (AMD) is an American multinational semiconductor company " +
                    "based in Santa Clara, California, that develops computer processors and related technologies for " +
                    "business and consumer markets. While it initially manufactured its own processors.",
                    15500,
                    "Sunnyvale, California, United States"),
                new Company("Nvidia Corporation", "Nvidia",
                    "Nvidia Corporation[note 1] (/ɛnˈvɪdiə/ en-VID-ee-ə) is an American multinational technology" +
                    " company incorporated in Delaware and based in Santa Clara, California.[2] It designs graphics processing units" +
                    " (GPUs) for the gaming and professional markets, as well as system on a chip units (SoCs) for the mobile computing and automotive market.",
                    18100,
                    "Santa Clara, California, United States")
            };
        }

        /*
        private static IEnumerable<JobOffer> GetPredefinedJobOffers()
        {
            return new List<JobOffer>
            {
                new JobOffer(
                    ".NET Junior Engineer",
                    @"Required experience and skills:
                      Very good knowledge of C#
                      Experience with MySQL/ MS SQL
                      Experience in object-oriented programming
                      At least 2 years' experience in a similar position 
                      Good command of English (at least B1 level)",
                    10500,
                    15200,
                    new Address(
                        new Country("Poland", "PL", "POL", 616),
                        "Kościerzyna",
                        "Długa 1",
                        "83-400",
                        54.121783M,
                        17.977314M),
                    new TechnologyType(".NET"),
                    new Seniority("Junior"),
                    new EmploymentType("B2B"))
            };
        }
        */
        
        /*
        private static IEnumerable<Address> GetPredefinedAddresses()
        {
            return new List<Address>();
        }
        */
    }
}