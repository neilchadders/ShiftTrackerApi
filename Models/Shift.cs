using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftTrackerApi.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Required] public DateTime Date { get; set; }
        [Required] public TimeSpan StartTime { get; set; }
        [Required] public TimeSpan EndTime { get; set; }

        [Range(1, 1000)]
        public decimal HourlyRate { get; set; }

        [NotMapped]
        public double TotalHours => (EndTime - StartTime).TotalHours;

        [NotMapped]
        public decimal Pay => (decimal)TotalHours * HourlyRate;
    }
}
