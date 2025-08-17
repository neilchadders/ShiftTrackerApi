using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftTrackerApi.Models;
using ShiftTrackerApi.Data;  // 

namespace ShiftTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShiftsController(AppDbContext context)
        {
            _context = context;
        }

        // POST /shifts
        [HttpPost]
        public async Task<ActionResult<Shift>> CreateShift([FromBody] Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShift), new { id = shift.Id }, shift);
        }

        // GET /shifts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return NotFound();
            return shift;
        }

        // GET /shifts
        [HttpGet]
        public async Task<IEnumerable<Shift>> GetAllShifts()
        {
            return await _context.Shifts.ToListAsync();
        }
    }
}
