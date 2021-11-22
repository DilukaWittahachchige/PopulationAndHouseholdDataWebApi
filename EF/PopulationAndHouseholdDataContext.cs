using EF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF
{
    public class PopulationAndHouseholdDataContext : DbContext
    {

        public DbSet<ActualDataEntity> ActualData { get; set; }
        public DbSet<EstimateDataEntity> EstimateData { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; private set; }

        public PopulationAndHouseholdDataContext()
        {

            DbPath = AppDomain.CurrentDomain.BaseDirectory;

            //if "bin" is present, remove all the path starting from "bin" word
            if (DbPath.Contains("bin"))
            {
                int index = DbPath.IndexOf("bin");
                DbPath = DbPath.Substring(0, index);
            }
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}Sqlite\\populationAndHousehold.db");
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //when Entity Class name and SQL table name not equal then need to match
            modelBuilder.Entity<ActualDataEntity>().ToTable("tbl_actual");
            modelBuilder.Entity<EstimateDataEntity>().ToTable("tbl_estimate");

        }

    }
}
