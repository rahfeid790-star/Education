namespace Education.Models.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade { get; set; } = string.Empty;
        public int SchoolId { get; set; }
    }
}
