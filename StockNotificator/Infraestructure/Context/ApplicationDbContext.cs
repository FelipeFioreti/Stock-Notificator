using Microsoft.EntityFrameworkCore;
using StockNotificator.Domain.Entities;

namespace StockNotificator.Infraestructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<UserStock> UserStocks => Set<UserStock>();
        public DbSet<AlertCondition> AlertConditions => Set<AlertCondition>();
        public DbSet<StockQuote> StockQuotes => Set<StockQuote>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<Stock>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Ticker).IsRequired().HasMaxLength(5);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Ticker).IsUnique();

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<UserStock>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.UserStocks)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Stock)
                      .WithMany(s => s.UserStocks)
                      .HasForeignKey(e => e.StockId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new { e.UserId, e.StockId }).IsUnique();

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<StockQuote>(entity => { 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.QuotedAt)
                    .IsRequired();
                entity.Property(e => e.Open)
                    .IsRequired()
                    .HasPrecision(18, 4);
                entity.Property(e => e.High)
                    .IsRequired()
                    .HasPrecision(18, 4);
                entity.Property(e => e.Low)
                    .IsRequired()
                    .HasPrecision(18, 4);
                entity.Property(e => e.Close)
                    .IsRequired() 
                .HasPrecision(18, 4);
                entity.HasOne(e => e.Stock)
                      .WithMany(s => s.StockQuotes)
                      .HasForeignKey(e => e.StockId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => new { e.StockId, e.QuotedAt }).IsUnique();
                entity.HasIndex(e => e.QuotedAt);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<AlertCondition>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Operator).IsRequired();
                entity.Property(e => e.TargetValue).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.HasOne(e => e.UserStock)
                      .WithMany(e => e.AlertConditions)
                      .HasForeignKey(e => e.UserStockId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

        }
    }
}
