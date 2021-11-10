using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SamuraiApp.Domain;

#nullable disable

namespace SamuraiApp.Data
{
    public partial class SamuraiContext : DbContext
    {


        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Samurai;Integrated Security=True")
                    .LogTo(Console.WriteLine, new [] {DbLoggerCategory.Database.Command.Name},
                    Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(s => s.Samurais)
                .UsingEntity<BattleSamurai>
                (bs => bs.HasOne<Battle>()
                .WithMany(),
                bs => bs.HasOne<Samurai>()
                .WithMany())
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");
        }

    }
}
