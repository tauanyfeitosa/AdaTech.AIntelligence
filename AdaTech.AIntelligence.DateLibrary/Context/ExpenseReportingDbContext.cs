using AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration;
using AdaTech.AIntelligence.Entities.Objects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AdaTech.AIntelligence.DateLibrary.Context
{
    /// <summary>
    /// Context for the application
    /// </summary>
    public class ExpenseReportingDbContext : IdentityDbContext<UserInfo>
    {
        /// <summary>
        /// Property for the expenses table
        /// </summary>
        public DbSet<Expense> Expenses { get; set; }

        /// <summary>
        /// Property for the roleRequirements table
        /// </summary>
        public DbSet<RoleRequirement> RoleRequirements { get; set; }

        /// <summary>
        /// Constructor for the context with ConnectionString for SQL Server
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ExpenseReporting;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        /// <summary>
        /// Method to apply the configurations for the entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new RoleRequirementConfiguration());
        }
    }
}
