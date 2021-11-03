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
    public class SubCategoriesController : ControllerBase
    {
        private readonly SqlContext _context;

        public SubCategoriesController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/SubCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            return await _context.SubCategories.ToListAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategory(int id, SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(subCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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




        // POST: api/SubCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategory>> PostSubCategory(CreateSubCategoryModel model)
        {

            // Kontrollerar att SubCategoryName inte är null och att CategoryId är större än 0
            if(!string.IsNullOrEmpty(model.SubCategoryName) && model.CategoryId > 0)
            {

                var _subcategory = await _context.SubCategories.Where(x => x.SubCategoryName.ToLower() == model.SubCategoryName.ToLower()).FirstOrDefaultAsync();

                // Går in och kollar om subcategory finns
                // Om den inte finns så skapar jag en
                if (_subcategory == null)
                {
                    var subCategory = new SubCategory
                    {
                        SubCategoryName = model.SubCategoryName,
                        CategoryId = model.CategoryId
                    };

                    _context.SubCategories.Add(subCategory);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetSubCategory", new { id = subCategory.Id }, subCategory);
                }
                // Om den redan finns
                // Skickar tillbaka felmeddelande 
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"SubCategory {model.SubCategoryName} already exsist" }));
            }

            // Skickar tillbaka felmeddelande om något gick fel i först if-satsen
            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Please fill in all required fields correctly" }));
        }

           




        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.Id == id);
        }
    }
}
