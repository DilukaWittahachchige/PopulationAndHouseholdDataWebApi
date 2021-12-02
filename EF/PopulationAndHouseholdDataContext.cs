using EF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF
{
    /// <summary>
    /// PopulationAndHouseholdDataContext EF Context
    /// </summary>
    public class PopulationAndHouseholdDataContext : DbContext
    {
        /// <summary>
        ///  Actual Data Entity
        /// </summary>
        public DbSet<ActualDataEntity> ActualData { get; set; }
        /// <summary>
        ///  Estimate Data Entity
        /// </summary>
        public DbSet<EstimateDataEntity> EstimateData { get; set; }
        /// <summary>
        ///  Codes Entity
        /// </summary>
        public DbSet<Code> Codes { get; set; }
        /// <summary>
        ///   User Entity
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        ///  Sql Lite DB path
        /// </summary>
        public string DbPath { get; private set; }

        public PopulationAndHouseholdDataContext()
        {
            //Load current base directory root path
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

        /// <summary>
        /// This method is called when the model for a derived context has been initialized,
        /// but before the model has been locked down and used to initialize the context.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //when Entity Class name and SQL table name not equal then need to match
            modelBuilder.Entity<ActualDataEntity>().ToTable("tbl_actual");
            modelBuilder.Entity<EstimateDataEntity>().ToTable("tbl_estimate");

        }

    }
}
