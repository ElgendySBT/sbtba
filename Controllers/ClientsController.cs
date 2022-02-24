using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Data;
using SBTBackEnd.Entities;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ClientsController : ControllerBase
    {
        private readonly DataContext _context;

        public ClientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SBTClient>>> GetSBTClients()
        {
            return await _context.SBTClients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SBTClient>> GetSBTClient(long id)
        {
            var sBTClient = await _context.SBTClients.FindAsync(id);

            if (sBTClient == null)
            {
                return NotFound();
            }

            return sBTClient;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSBTClient(long id, SBTClient sBTClient)
        {
            if (id != sBTClient.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(sBTClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SBTClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SBTClient>> PostSBTClient(SBTClient sBTClient)
        {
            _context.SBTClients.Add(sBTClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSBTClient", new { id = sBTClient.ClientId }, sBTClient);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSBTClient(long id)
        {
            var sBTClient = await _context.SBTClients.FindAsync(id);
            if (sBTClient == null)
            {
                return NotFound();
            }

            _context.SBTClients.Remove(sBTClient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SBTClientExists(long id)
        {
            return _context.SBTClients.Any(e => e.ClientId == id);
        }
    }
}
