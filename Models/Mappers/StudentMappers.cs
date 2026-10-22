using Education.Models;
using Education.Models.DTOs;

namespace Education.Models.Mappers
{
    public static class StudentMappers
    {
        public static StudentDto ToStudentDto(this Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Grade = student.Grade,
                SchoolId = student.SchoolId
            };
        }

        public static Student ToStudentFromRequestDto(this StudentRequestDto dto, int schoolId)
        {
            return new Student
            {
                Name = dto.Name,
                Age = dto.Age,
                Grade = dto.Grade,
                SchoolId = schoolId
            };
        }
    }
}
