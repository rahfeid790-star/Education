using System.ComponentModel.DataAnnotations;
namespace Education.Models.DTOs

{
    public class TeacherRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Range(1000, 100000, ErrorMessage = "Salary must be between 1000 and 100000")]
        public decimal Salary { get; set; }

        [Required]
        public int SchoolId { get; set; }
    }
}
    

