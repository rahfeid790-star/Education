using Education.Models;
using Education.Models.DTOs;

namespace Education.Models.Mappers
{
    public static class SubjectMappers
    {
        public static SubjectDto ToSubjectDto(this Subject subject)
        {
            return new SubjectDto
            {
                Id = subject.Id,
                Title = subject.Title,
                Code = subject.Code,
                CreditHours = subject.CreditHours,
                TeacherId = subject.TeacherId
            };
        }

        public static Subject ToSubjectFromRequestDto(this SubjectRequestDto dto, int teacherId)
        {
            return new Subject
            {
                Title = dto.Title,
                Code = dto.Code,
                CreditHours = dto.CreditHours,
                TeacherId = teacherId
            };
        }
    }
}
