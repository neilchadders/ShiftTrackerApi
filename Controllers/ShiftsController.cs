using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftTrackerApi.Data;
using ShiftTrackerApi.Models;

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

        // GET /shifts?month=YYYY-MM
        [HttpGet]
        public async Task<IActionResult> GetShifts([FromQuery] string month)
        {
            if (string.IsNullOrEmpty(month))
                return BadRequest("Month is required in format YYYY-MM");

            // Parse month
            var parts = month.Split('-');
            if (parts.Length != 2) return BadRequest("Invalid month format");
            int year = int.Parse(parts[0]);
            int monthNum = int.Parse(parts[1]);

            var shifts = await _context.Shifts
                .Where(s => s.Date.Year == year && s.Date.Month == monthNum)
                .ToListAsync();

            // Calculate total hours
            double totalHours = shifts.Sum(s => (s.EndTime - s.StartTime).TotalHours);

            return Ok(new { shifts, totalHours });
        }

        // POST /shifts
        [HttpPost]
        public async Task<IActionResult> AddShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(shift);
        }
    }
}
