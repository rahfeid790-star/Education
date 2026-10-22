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
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _context.Subjects
                .Where(s => !s.IsDeleted)
                .Select(s => s.ToSubjectDto())
                .ToListAsync();

            return Ok(subjects);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null || subject.IsDeleted)
            {
                return NotFound($"Subject with Id {id} not found.");
            }

            return Ok(subject.ToSubjectDto());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var teacherExists = await _context.Teachers.AnyAsync(t => t.Id == dto.TeacherId && !t.IsDeleted);
            if (!teacherExists)
            {
                return BadRequest("Associated Teacher not found.");
            }

            var subjectModel = dto.ToSubjectFromRequestDto(dto.TeacherId);
            await _context.Subjects.AddAsync(subjectModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = subjectModel.Id }, subjectModel.ToSubjectDto());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SubjectRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null || subject.IsDeleted)
            {
                return NotFound($"Subject with Id {id} not found.");
            }

            subject.Title = dto.Title;
            subject.Code = dto.Code;
            subject.CreditHours = dto.CreditHours;
            subject.TeacherId = dto.TeacherId;

            await _context.SaveChangesAsync();
            return Ok(subject.ToSubjectDto());
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null || subject.IsDeleted)
            {
                return NotFound($"Subject with Id {id} not found.");
            }

            subject.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Subject soft-deleted successfully." });
        }
    }
}
