using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_store.Data;
using e_store.Dto;
using e_store.Models;

namespace e_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryDtoesController : ControllerBase
    {
        private readonly EStoreDbContext _context;

        public CategoryDtoesController(EStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoryDtoes
        [HttpGet]
  
        public IQueryable<CategoryDto> GetAll()
        {
            var categ = from b in _context.Categories
                      select new CategoryDto()
                      {
                        CategoryId=b.CategoryId,
                        Name=b.Name


                      };
            return categ;
        }


        // GET: api/CategoryDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryDto(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            // تحويل الكائن category إلى CategoryDto
            var categoryDto = new CategoryDto
            {
                CategoryId=category.CategoryId,
                Name = category.Name,
                // أضف الحقول الأخرى المطلوبة للتحويل
            };

            return categoryDto;
        }


        // PUT: api/CategoryDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            if (id != dto.CategoryId)
            {
                return BadRequest();
            }

            var custom = await _context.Categories.FindAsync(id);
            if (custom == null)
            {
                return NotFound();
            }
            custom.Name = dto.Name;
       

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CategoryDtoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/CategoryDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCategoryDto(CategoryDto categoryDto)
        {
            var dto = new Category()

            {
               CategoryId=categoryDto.CategoryId,
               Name=categoryDto.Name,

            };
            await _context.AddAsync(dto);
            _context.SaveChanges();
            return Ok(dto);
        }

        // DELETE: api/CategoryDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryDto(int id)

        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categoryDto = await _context.Categories.FindAsync(id);
            if (categoryDto == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryDtoExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }



}
