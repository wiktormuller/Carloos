using JobJetRestApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobJetRestApi.Infrastructure.Persistence.DbContexts
{
    public class JobJetDbContext : DbContext
    {
        public JobJetDbContext(DbContextOptions<JobJetDbContext> options) : base(options)
        {
        }

        // The OnConfiguring() method allows us to select and configure the data source to be used with a context using DbContextOptionsBuilder.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        // The OnModelCreating() method allows us to configure the model using ModelBuilder Fluent API.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Entities
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<TechnologyType> TechnologyTypes { get; set; }
        public DbSet<CountryIso> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Seniority> SeniorityLevels { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
    }
}