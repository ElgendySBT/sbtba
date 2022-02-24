using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Data;
using SBTBackEnd.Entities;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly DataContext _context;

        public FeedbackController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackMessage>>> GetFeedbackMessage()
        {
            return await _context.FeedbackMessages.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackMessage>> GetFeedbackMessage(int id)
        {
            var feedbackMessage = await _context.FeedbackMessages.FindAsync(id);

            if (feedbackMessage == null)
            {
                return NotFound();
            }

            return feedbackMessage;
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackMessage(int id, FeedbackMessage feedbackMessage)
        {
            if (id != feedbackMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedbackMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackMessageExists(id))
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

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedbackMessage>> PostFeedbackMessage(FeedbackMessage feedbackMessage)
        {
            _context.FeedbackMessages.Add(feedbackMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbackMessage", new { id = feedbackMessage.Id }, feedbackMessage);
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackMessage(int id)
        {
            var feedbackMessage = await _context.FeedbackMessages.FindAsync(id);
            if (feedbackMessage == null)
            {
                return NotFound();
            }

            _context.FeedbackMessages.Remove(feedbackMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackMessageExists(int id)
        {
            return _context.FeedbackMessages.Any(e => e.Id == id);
        }
    }
}
