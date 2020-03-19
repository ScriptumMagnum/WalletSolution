using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    Title = "Рубль",
                    Code = "RUB"
                },
                new Currency
                {
                    Id = 2,
                    Title = "Евро",
                    Code = "EUR"
                },
                new Currency
                {
                    Id = 3,
                    Title = "Доллар",
                    Code = "USD"
                }
            );
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<WalletRecord> WalletRecords { get; set; }
    }
}
