using EF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF
{
    public class PopulationAndHouseholdDataContext : DbContext
    {
        public PopulationAndHouseholdDataContext(DbContextOptions<PopulationAndHouseholdDataContext> options) : base(options)
        {
        }

        public DbSet<ActualDataEntity> ActualData { get; set; }
        public DbSet<EstimateDataEntity> EstimateData { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; private set; }

        public PopulationAndHouseholdDataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}populationAndHouseholdData.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //when Entity Class name and SQL table name not equal then need to match
            modelBuilder.Entity<ActualDataEntity>().ToTable("tblActuResidentPopulation");
            modelBuilder.Entity<EstimateDataEntity>().ToTable("tblExtiResidentPopulation");
            modelBuilder.Entity<Code>().ToTable("tblCode");
            modelBuilder.Entity<User>().ToTable("tblUser");
        }

    }
}
