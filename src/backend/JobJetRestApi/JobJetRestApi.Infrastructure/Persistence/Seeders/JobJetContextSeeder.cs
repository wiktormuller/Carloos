using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Repositories;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Persistence.Seeders
{
    public class JobJetContextSeeder
    {
        public static async Task SeedAsync(JobJetDbContext context, 
            IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IMediator mediator,
            int retryCounter = 0)
        {
            var retryForAvailability = retryCounter;
            
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }

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
                
                if (!await context.Users.AnyAsync())
                {
                    await userRepository.CreateAsync(GetPredefinedAdminAccount(), "Password123!");
                    await context.SaveChangesAsync();
                }

                if (!await context.Roles.AnyAsync())
                {
                    await roleRepository.CreateAsync(new Role("Administrator"));
                    await roleRepository.CreateAsync(new Role("User"));
                }
                
                if (!await context.UserRoles.AnyAsync())
                {
                    var adminRole = await roleRepository.GetByNameAsync("Administrator");
                    var userRole = await roleRepository.GetByNameAsync("User");
                    var user = await userRepository.GetByEmailAsync("ceo@jobjet.com");
                    await userRepository.AssignRoleToUser(user, adminRole);
                    await userRepository.AssignRoleToUser(user, userRole);
                }

                if (!await context.JobOffers.AnyAsync())
                {
                    var jobOffersCommands = GetPredefinedJobOffers();
                    foreach (var command in jobOffersCommands)
                    {
                        await mediator.Send(command);
                    }
                }
            }
            catch (Exception e)
            {
                if (retryForAvailability >= 10) throw;
                retryForAvailability++;
                await SeedAsync(context, userRepository, roleRepository, mediator, retryForAvailability);
                throw;
            }
        }

        private static User GetPredefinedAdminAccount()
        {
            var user = new User("ceo@jobjet.com", "CEO");
            var companies = GetPredefinedCompanies();
            
            foreach (var company in companies)
            {
                user.AddCompany(company);
            }

            return user;
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
                new Country("Poland", 
                    "PL", 
                    "POL", 
                    616, 
                    52.006376M, 
                    19.025167M),
                new Country("United Kingdom", 
                    "GB", 
                    "GBR", 
                    826, 
                    54.00366M, 
                    -2.547855M),
                new Country("Germany", 
                    "DE", 
                    "DEU", 
                    276, 
                    52.531677M, 
                    13.381777M),
                new Country("Switzerland",
                    "CH", 
                    "CHE", 
                    756, 
                    46.80111M, 
                    8.22667M),
                new Country("Belgium", 
                    "BE", 
                    "BEL", 
                    056, 
                    50.503887M, 
                    4.469936M)
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
                new TechnologyType("JavaScript"),
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

        private static IEnumerable<CreateJobOfferCommand> GetPredefinedJobOffers()
        {
            return new List<CreateJobOfferCommand>
            {
                new CreateJobOfferCommand(
                    1, 
                    1, 
                    "Intern JavaScript Developer", 
                    "We are hiring a intern JS Dev for our frontend project. If you are interested, lets us know.",
                    3000,
                    5000,
                    new List<int> { 1, 2 },
                    1,
                    2,
                    "Łódź",
                    "Cyganka 51",
                    "94-221",
                    1,
                    1,
                    "FullyRemote"
                ),
                new CreateJobOfferCommand(
                    1, 
                    2, 
                    "Junior .NET Engineer", 
                    "If you want to join to a team full of passion and learn the newest technologies, like .NET Core - just apply to us.",
                    4000,
                    10000,
                    new List<int> { 7 },
                    2,
                    2,
                    "Warszawa",
                    "Szprotawska 130",
                    "03-879",
                    1,
                    1,
                    "Hybrid"
                ),
                new CreateJobOfferCommand(
                    1, 
                    3, 
                    "UX/UI Designer", 
                    "Join to our company where we create the most advanced user interfaces used by over 1 000 000 people around the world.",
                    10000,
                    14000,
                    new List<int> { 14, 2, 24 },
                    3,
                    1,
                    "Gdańsk",
                    "Kowale 25",
                    "80-180",
                    1,
                    1,
                    "Office"
                ),
                new CreateJobOfferCommand(
                    1, 
                    1, 
                    "Senior / Solution Architect", 
                    "We are looking for a person with broad knowledge about architecture and developing highly scalable cloud systems based on microservices written in Go language.",
                    15000,
                    25000,
                    new List<int> { 23, 20, 21 },
                    4,
                    1,
                    "Szczecin",
                    "Stołczyńska 60",
                    "71-868",
                    1,
                    1,
                    "FullyRemote"
                ),
                new CreateJobOfferCommand(
                    1, 
                    2, 
                    "DevOps Engineer", 
                    "We are looking for person who is able to configure things like Azure DevOps and manage the whole flow that is used for CI/CD.",
                    9000,
                    12000,
                    new List<int> { 12, 13 },
                    3,
                    3,
                    "Gliwice",
                    "Al. Przyjaźni 135",
                    "44-100",
                    1,
                    1,
                    "Hybrid"
                ),
                new CreateJobOfferCommand(
                    1, 
                    3, 
                    "Position for Tester is open!", 
                    "If you are skilled at writing automatic tests, like E2E, integration tests etc in Python. We can offer you a job for one of our clients.",
                    3000,
                    5500,
                    new List<int> { 5, 11 },
                    2,
                    2,
                    "Kraków",
                    "Rybaki 60",
                    "31-067",
                    1,
                    1,
                    "FullyRemote"
                ),
                new CreateJobOfferCommand(
                    1, 
                    1, 
                    "Offer for Unity Programmer", 
                    "Apply to us if you are passtionate about creating 3D and 2D games for desktop in Unity environment.",
                    2000,
                    4000,
                    new List<int> { 7, 16 },
                    1,
                    3,
                    "Łódź",
                    "Moniuszki Stanisława 100",
                    "90-110",
                    1,
                    1,
                    "FullyRemote"
                ),
                new CreateJobOfferCommand(
                    1, 
                    2, 
                    "Scala Senior Developer", 
                    "We are hiring senior Scala developer for our projects that use techniques like data processing, distributed computing, and web development. Every experience in building scalable microservices is just an extra asset.",
                    25000,
                    28000,
                    new List<int> { 8, 24 },
                    4,
                    2,
                    "Leargybreck",
                    "28 Park Avenue",
                    "PA60 6YS",
                    2,
                    1,
                    "Office"
                ),
                new CreateJobOfferCommand(
                    1, 
                    3, 
                    "Ruby Mid Developer Position", 
                    "We are hiring ruby mid developer. If you have commercial experience in creating web APIs in REST - apply.",
                    10000,
                    12000,
                    new List<int> { 4 },
                    3,
                    2,
                    "Hamburg Eißendorf",
                    "Boxhagener Str. 15",
                    "21077",
                    3,
                    1,
                    "FullyRemote"
                ),
                new CreateJobOfferCommand(
                    1, 
                    1, 
                    "Mid PHP Web Developer", 
                    "If you have more than 3 years experience in PHP and backend development apply for this job offer!",
                    10000,
                    13000,
                    new List<int> { 2, 3 },
                    3,
                    2,
                    "Oberfrick",
                    "Clius 5",
                    "5073",
                    4,
                    1,
                    "Hybrid"
                )
            };
        }
    }
}