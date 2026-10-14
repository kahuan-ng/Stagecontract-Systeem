using Microsoft.EntityFrameworkCore;
using WSI.Goat.InternshipContracts.Core.Entities;
using WSI.Goat.InternshipsContracts.Web.Data;
using WSI.Goat.IntershipsContracts.Core.Entities;

namespace WSI.Goat.InternshipContracts.Web.Data
{
    public class InternshipContractsDbContext : DbContext
    {
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<InternshipContract> InternshipContracts { get; set; }
        public DbSet<InternshipType> InternshipTypes { get; set; }
        public DbSet<StageOrganization> StageOrganizations { get; set; }
        
        public InternshipContractsDbContext(DbContextOptions options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataSeeder.Seeder(modelBuilder);
        }
    }
}



