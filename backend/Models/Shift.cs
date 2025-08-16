using System;

namespace Backend.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal HourlyRate { get; set; }

        // Helper: calculate total hours worked
        public double TotalHours => (EndTime - StartTime).TotalHours;

        // Helper: calculate pay
        public decimal Pay => (decimal)TotalHours * HourlyRate;
    }
}
