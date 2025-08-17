using Microsoft.EntityFrameworkCore;
using ShiftTrackerApi.Models;

namespace ShiftTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
    }
}
