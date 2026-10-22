namespace Education.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade { get; set; } = string.Empty;

        
        public int SchoolId { get; set; }
        public School? School { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
    

