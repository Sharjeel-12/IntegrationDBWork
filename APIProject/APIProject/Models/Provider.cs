namespace APIProject.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }
}
