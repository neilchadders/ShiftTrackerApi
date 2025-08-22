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

                // PUT /shifts/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift updatedShift)
{
    if (id != updatedShift.Id)
        return BadRequest("ID mismatch");

    var existingShift = await _context.Shifts.FindAsync(id);
    if (existingShift == null)
        return NotFound();

    // Update fields
    existingShift.Date = updatedShift.Date;
    existingShift.StartTime = updatedShift.StartTime;
    existingShift.EndTime = updatedShift.EndTime;
    existingShift.HourlyRate = updatedShift.HourlyRate;

    await _context.SaveChangesAsync();
    return Ok(existingShift);
}

    }
}
