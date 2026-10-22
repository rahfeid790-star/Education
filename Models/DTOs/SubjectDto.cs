namespace Education.Models.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public int TeacherId { get; set; }
    }
}
