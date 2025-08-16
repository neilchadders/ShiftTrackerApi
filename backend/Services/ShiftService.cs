using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ShiftService
    {
        private readonly AppDbContext _context;

        public ShiftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> GetAllAsync() =>
            await _context.Shifts.ToListAsync();

        public async Task<Shift?> GetByIdAsync(int id) =>
            await _context.Shifts.FindAsync(id);

        public async Task<Shift> CreateAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return false;
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
