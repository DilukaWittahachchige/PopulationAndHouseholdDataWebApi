using EF.Models;
using Microsoft.EntityFrameworkCore;

namespace EF
{
    public class PopulationAndHouseholdDataEntities : DbContext
    {
        public PopulationAndHouseholdDataEntities(DbContextOptions<PopulationAndHouseholdDataEntities> options) : base(options)
        {
        }

        public DbSet<ActualData> ActualData { get; set; }
        public DbSet<EstimateData> EstimateData { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //when Entity Class name and SQL table name not equal then need to match
            modelBuilder.Entity<ActualData>().ToTable("tblActuResidentPopulation");
            modelBuilder.Entity<EstimateData>().ToTable("tblExtiResidentPopulation");
            modelBuilder.Entity<Code>().ToTable("tblCode");
            modelBuilder.Entity<User>().ToTable("tblUser");
        }

    }
}
