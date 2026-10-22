using Education.Models;
using Education.Models.DTOs;

namespace Education.Models.Mappers
{
    public static class TeacherMappers
    {
        public static TeacherDto ToTeacherDto(this Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                Salary = teacher.Salary,
                SchoolId = teacher.SchoolId
            };
        }

        public static Teacher ToTeacherFromRequestDto(this TeacherRequestDto dto, int schoolId)
        {
            return new Teacher
            {
                Name = dto.Name,
                Email = dto.Email,
                Salary = dto.Salary,
                SchoolId = schoolId
            };
        }
    }
}