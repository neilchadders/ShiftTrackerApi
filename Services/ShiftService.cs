using ShiftTrackerApi.Data;
using ShiftTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ShiftTrackerApi.Services
{
    public class ShiftService
    {
        private readonly AppDbContext _context;

        public ShiftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> AddShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<List<Shift>> GetShiftsAsync()
        {
            return await _context.Shifts.ToListAsync();
        }
    }
}
