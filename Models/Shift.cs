using System.ComponentModel.DataAnnotations;

namespace ShiftTrackerApi.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public TimeSpan EndTime { get; set; }

        [Range(1, 1000, ErrorMessage = "Hourly rate must be between 1 and 1000")]
        public decimal HourlyRate { get; set; }

        // Computed properties
        public double TotalHours => (EndTime - StartTime).TotalHours;
        public decimal Pay => (decimal)TotalHours * HourlyRate;
    }
}
