using AdaTech.AIntelligence.DateLibrary.EntityFramework.Configuration;
using AdaTech.AIntelligence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdaTech.AIntelligence.DateLibrary.Context
{
    public class ExpenseReportingDbContext : IdentityDbContext<UserInfo>
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpenseReportingDbContext(DbContextOptions<ExpenseReportingDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        }
    }
}
