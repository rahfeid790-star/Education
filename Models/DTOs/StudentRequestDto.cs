using System.ComponentModel.DataAnnotations;

namespace Education.Models.DTOs
{
    public class StudentRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(5, 100, ErrorMessage = "Age must be between 5 and 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public string Grade { get; set; } = string.Empty;

        [Required]
        public int SchoolId { get; set; }
    }
}
