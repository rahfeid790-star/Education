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
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students
                .Where(s => !s.IsDeleted)
                .Select(s => s.ToStudentDto())
                .ToListAsync();

            return Ok(students);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null || student.IsDeleted)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            return Ok(student.ToStudentDto());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentRequestDto dto)
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

            var studentModel = dto.ToStudentFromRequestDto(dto.SchoolId);
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel.ToStudentDto());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null || student.IsDeleted)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            student.Name = dto.Name;
            student.Age = dto.Age;
            student.Grade = dto.Grade;
            student.SchoolId = dto.SchoolId;

            await _context.SaveChangesAsync();
            return Ok(student.ToStudentDto());
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null || student.IsDeleted)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            student.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student soft-deleted successfully." });
        }
    }
}
