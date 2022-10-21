using CollegePick10V2.Configuration;
using CollegePick10V2.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegePick10V2.Entities
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<CurrentWeek> CurrentWeeks { get; set; }
        public DbSet<GameArchive> GameArchives { get; set; }
        public DbSet<PickType> PickTypes { get; set; }
        public DbSet<Pick> Picks { get; set; }
        public DbSet<PickArchive> PickArchives { get; set; }
    }
}
