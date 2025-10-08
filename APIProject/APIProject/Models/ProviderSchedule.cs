namespace APIProject.Models
{
    public class ProviderSchedule
    {
        public int ScheduleId { get; set; }
        public DateOnly ScheduleDate { get; set; }
        public TimeOnly ScheduleTime { get; set; }
        public int Duration { get; set; }
        public bool Status { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int ResourceId { get; set; }
        public int LocationId { get; set; }


    }
}
