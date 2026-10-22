using Education.Data;
using Education.Models;
using Education.Models.DTOs;
using Education.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _context.Teachers
                .Where(t => !t.IsDeleted)
                .Select(t => t.ToTeacherDto())
                .ToListAsync();

            return Ok(teachers);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null || teacher.IsDeleted)
            {
                return NotFound($"Teacher with Id {id} not found.");
            }

            return Ok(teacher.ToTeacherDto());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schoolExists = await _context.Schools.AnyAsync(s => s.Id == dto.SchoolId && !s.IsDeleted);
            if (!schoolExists)
            {
                return BadRequest("Associated School not found.");
            }

            var teacherModel = dto.ToTeacherFromRequestDto(dto.SchoolId);
            await _context.Teachers.AddAsync(teacherModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = teacherModel.Id }, teacherModel.ToTeacherDto());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TeacherRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null || teacher.IsDeleted)
            {
                return NotFound($"Teacher with Id {id} not found.");
            }

            teacher.Name = dto.Name;
            teacher.Email = dto.Email;
            teacher.Salary = dto.Salary;
            teacher.SchoolId = dto.SchoolId;

            await _context.SaveChangesAsync();
            return Ok(teacher.ToTeacherDto());
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null || teacher.IsDeleted)
            {
                return NotFound($"Teacher with Id {id} not found.");
            }

            teacher.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher soft-deleted successfully." });
        }
    }
}
