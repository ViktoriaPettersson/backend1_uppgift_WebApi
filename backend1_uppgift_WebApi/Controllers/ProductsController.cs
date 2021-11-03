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
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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



        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductModel model)
        {


            // Kontrollerar att ProductName inte är null och att SubCategoryId är större än 0
            if (!string.IsNullOrEmpty(model.ProductName) && model.SubCategoryId > 0)
            {

                var _product = await _context.Products.Where(x => x.ProductName.ToLower() == model.ProductName.ToLower()).FirstOrDefaultAsync();

                // Går in och kollar om product finns
                // Om den inte finns så skapar jag en
                if (_product == null)
                {
                    var product = new Product
                    {
                       ProductName = model.ProductName,
                       ShortDescription = model.ShortDescription,
                       LongDescription = model.LongDescription,
                       Price = model.Price,
                       ImageUrl = model.ImageUrl,
                       SubCategoryId = model.SubCategoryId
             
                    };

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
                }
                // Om den redan finns
                // Skickar tillbaka felmeddelande 
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Product {model.ProductName} already exsist" }));
            }

            // Skickar tillbaka felmeddelande om något gick fel i först if-satsen
            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Please fill in all required fields correctly" }));
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
