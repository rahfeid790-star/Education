using System.ComponentModel.DataAnnotations;

namespace Education.Models.DTOs
{
    public class SubjectRequestDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Credit hours must be between 1 and 10")]
        public int CreditHours { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}
