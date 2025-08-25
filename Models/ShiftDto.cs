namespace ShiftTrackerApi.Models
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;      // "yyyy-MM-dd"
        public string StartTime { get; set; } = string.Empty; // "HH:mm"
        public string EndTime { get; set; } = string.Empty;   // "HH:mm"
        public decimal HourlyRate { get; set; }
        public double TotalHours { get; set; }
        public decimal Pay { get; set; }

        public static ShiftDto FromShift(Shift shift)
        {
            return new ShiftDto
            {
                Id = shift.Id,
                Date = shift.Date.ToString("yyyy-MM-dd"),
                StartTime = shift.StartTime.ToString(@"hh\:mm"),
                EndTime = shift.EndTime.ToString(@"hh\:mm"),
                HourlyRate = shift.HourlyRate,
                TotalHours = shift.TotalHours,
                Pay = shift.Pay
            };
        }
    }
}
