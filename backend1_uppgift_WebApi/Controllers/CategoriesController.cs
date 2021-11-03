using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend1_uppgift_WebApi.Data;
using backend1_uppgift_WebApi.Models;
using Newtonsoft.Json;

namespace backend1_uppgift_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoriesController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // Använder mig av min CreateCategoryModel i controllern för Category.cs
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryModel model)
        {
            // Kollar att CategoryName inte är tomt
            if(!string.IsNullOrEmpty(model.CategoryName))

            {
                // Kollar först om kategorin finns eller inte

                var _category = await _context.Categories.Where(x => x.CategoryName.ToLower() == model.CategoryName.ToLower()).FirstOrDefaultAsync();

                if (_category == null)

                {
                    // Om kategorin inte finns så skapar jag den 
                    var category = new Category
                    {
                        CategoryName = model.CategoryName
                    };

                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetCategory", new { id = category.Id }, category);
                }


                // Om den redan finns
                // Skickar tillbaka felmeddelande 
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Category {model.CategoryName} already exsist" }));
            }

            // Om den är tom, skicka meddelande
            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Please fill in all required fields correctly" }));
        }


        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
