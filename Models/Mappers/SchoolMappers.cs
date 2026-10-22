using Education.Models;
using Education.Models.DTOs;

namespace Education.Models.Mappers
{
    public static class SchoolMappers
    {
        public static SchoolDto ToSchoolDto(this School school)
        {
            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                Phone = school.Phone
            };
        }

        public static School ToSchoolFromRequestDto(this SchoolRequestDto dto)
        {
            return new School
            {
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone
            };
        }
    }
}