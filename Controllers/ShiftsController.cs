using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftTrackerApi.Data;
using ShiftTrackerApi.Models;

namespace ShiftTrackerApi.Controllers
{
    [ApiController]
    [Route("shifts")]
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

            var parts = month.Split('-');
            if (parts.Length != 2) return BadRequest("Invalid month format");

            int year = int.Parse(parts[0]);
            int monthNum = int.Parse(parts[1]);

            var shifts = await _context.Shifts
                .Where(s => s.Date.Year == year && s.Date.Month == monthNum)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.StartTime)
                .ToListAsync();

            double totalHours = shifts.Sum(s => (s.EndTime - s.StartTime).TotalHours);

            // Map to DTOs
            var dtoList = shifts.Select(ShiftDto.FromShift).ToList();

            return Ok(new { shifts = dtoList, totalHours });
        }

        [HttpPost]
        public async Task<IActionResult> AddShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (shift.EndTime <= shift.StartTime)
                return BadRequest("End time must be after start time.");

            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            return Ok(ShiftDto.FromShift(shift));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift updatedShift)
        {
            if (id != updatedShift.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updatedShift.EndTime <= updatedShift.StartTime)
                return BadRequest("End time must be after start time.");

            var existingShift = await _context.Shifts.FindAsync(id);
            if (existingShift == null)
                return NotFound();

            existingShift.Date = updatedShift.Date;
            existingShift.StartTime = updatedShift.StartTime;
            existingShift.EndTime = updatedShift.EndTime;
            existingShift.HourlyRate = updatedShift.HourlyRate;

            await _context.SaveChangesAsync();

            return Ok(ShiftDto.FromShift(existingShift));
        }

        // DELETE /shifts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return NotFound();

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
