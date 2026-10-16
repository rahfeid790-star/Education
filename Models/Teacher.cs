namespace Education.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        
        public int SchoolId { get; set; }
        public School? School { get; set; }

        
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
    

