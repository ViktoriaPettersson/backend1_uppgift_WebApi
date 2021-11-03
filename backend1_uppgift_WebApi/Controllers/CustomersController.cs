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
    public class CustomersController : ControllerBase
    {
        private readonly SqlContext _context;

        public CustomersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // Använder min CreateCustomerModell för att skapa en customer

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CreateCustomerModel model)
        {
            // Gör en sökning i databasen där email ska vara lika med model.email
            var _customer = await _context.Customers.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            // Om _customer är null då vill jag skapa en 
            if(_customer == null)
            {
                var customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CustomerHash = model.CustomerHash,
                    Address = new Address
                    {
                        AddressLine = model.AddressLine,
                        City = model.City,
                        ZipCode = model.ZipCode
                    }

                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            // Om den redan finns
            // Skickar tillbaka felmeddelande 
            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Email {model.Email} already exsist" }));
        }
         




        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
