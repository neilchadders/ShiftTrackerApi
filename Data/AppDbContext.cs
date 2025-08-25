using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShiftTrackerApi.Models;

namespace ShiftTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shift> Shifts => Set<Shift>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var timeSpanToTicks = new TimeSpanToTicksConverter();

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(s => s.StartTime)
                      .HasConversion(timeSpanToTicks)   // TimeSpan <-> long
                      .HasColumnType("INTEGER");

                entity.Property(s => s.EndTime)
                      .HasConversion(timeSpanToTicks)
                      .HasColumnType("INTEGER");

                // Optional but nice: speed up your ordering/filtering
                entity.HasIndex(s => new { s.Date, s.StartTime });
            });
        }
    }
}
