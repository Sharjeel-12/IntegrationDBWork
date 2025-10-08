namespace APIProject.Dtos
{
    public class CreateProviderScheduleDto
    {
        public DateOnly ScheduleDate { get; set; }
        public TimeOnly ScheduleTime { get; set; }
        public int Duration { get; set; }
        public bool Status { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public int ResourceId { get; set; }
        public int LocationId { get; set; }
    }
    public class UpdateProviderScheduleDto
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
