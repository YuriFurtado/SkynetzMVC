using Microsoft.EntityFrameworkCore;
using System;

namespace SkynetzMVC.Models
{
    public class SkynetzDbContext : DbContext
    {
        public SkynetzDbContext(DbContextOptions<SkynetzDbContext> options) : base(options)
        {
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = 2,
                    Name = "FaleMais 60",
                    FreeMinutes = 60
                },
                new Plan
                {
                    Id = 3,
                    Name = "FaleMais 120",
                    FreeMinutes = 120
                }
            );

            modelBuilder.Entity<Tariff>().HasData(
                new Tariff
                {
                    Id = 2,
                    Source = "016",
                    Destination = "011",
                    MinuteValue = 2.90
                },
                new Tariff
                {
                    Id = 3,
                    Source = "011",
                    Destination = "017",
                    MinuteValue = 1.70
                },
                new Tariff
                {
                    Id = 4,
                    Source = "017",
                    Destination = "011",
                    MinuteValue = 2.70
                },
                new Tariff
                {
                    Id = 5,
                    Source = "011",
                    Destination = "018",
                    MinuteValue = 0.90
                },
                new Tariff
                {
                    Id = 6,
                    Source = "018",
                    Destination = "011",
                    MinuteValue = 1.90
                }
            );
        }
    }
}
