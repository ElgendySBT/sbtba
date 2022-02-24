using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Data;
using SBTBackEnd.Entities.Product;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public SubProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SubProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubProduct>>> GetSubProducts()
        {
            return await _context.SubProducts.ToListAsync();
        }

        // GET: api/SubProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubProduct>> GetSubProduct(int id)
        {
            var subProduct = await _context.SubProducts.FindAsync(id);

            if (subProduct == null)
            {
                return NotFound();
            }

            return subProduct;
        }

        // PUT: api/SubProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubProduct(int id, SubProduct subProduct)
        {
            if (id != subProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(subProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubProductExists(id))
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

        // POST: api/SubProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubProduct>> PostSubProduct(SubProduct subProduct)
        {
            _context.SubProducts.Add(subProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubProduct", new { id = subProduct.Id }, subProduct);
        }

        // DELETE: api/SubProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubProduct(int id)
        {
            var subProduct = await _context.SubProducts.FindAsync(id);
            if (subProduct == null)
            {
                return NotFound();
            }

            _context.SubProducts.Remove(subProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubProductExists(int id)
        {
            return _context.SubProducts.Any(e => e.Id == id);
        }
    }
}
