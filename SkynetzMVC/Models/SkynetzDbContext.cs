using Microsoft.EntityFrameworkCore;
using System;

namespace SkynetzMVC.Models
{
    public class SkynetzDbContext : DbContext
    {
        public SkynetzDbContext(DbContextOptions<SkynetzDbContext> options) : base(options)
        {

        }

        public SkynetzDbContext()
        {

        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

    }
}
