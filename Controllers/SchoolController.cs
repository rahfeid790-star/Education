using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Education.Data;
using Education.Models;
using Education.Models.DTOs;
using Education.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Education.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SchoolController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _context.Schools
                .Where(s => !s.IsDeleted)
                .Select(s => s.ToSchoolDto())
                .ToListAsync();

            return Ok(schools);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null || school.IsDeleted)
            {
                return NotFound($"School with Id {id} not found.");
            }

            return Ok(school.ToSchoolDto());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schoolModel = dto.ToSchoolFromRequestDto();
            await _context.Schools.AddAsync(schoolModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = schoolModel.Id }, schoolModel.ToSchoolDto());
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SchoolRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var school = await _context.Schools.FindAsync(id);
            if (school == null || school.IsDeleted)
            {
                return NotFound($"School with Id {id} not found.");
            }

            school.Name = dto.Name;
            school.Address = dto.Address;
            school.Phone = dto.Phone;

            await _context.SaveChangesAsync();
            return Ok(school.ToSchoolDto());
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null || school.IsDeleted)
            {
                return NotFound($"School with Id {id} not found.");
            }

            school.IsDeleted = true; 
            await _context.SaveChangesAsync();

            return Ok(new { message = "School soft-deleted successfully." });
        }
    }
}
    

