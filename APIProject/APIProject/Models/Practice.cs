namespace APIProject.Models
{
    public class Practice
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
    }

    public class PracticeLocation
    {
        public int LocationId { get; set; }
        public int PracticeId { get; set; }
        public string Address { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string POS { get; set; } = string.Empty;

    }
}
