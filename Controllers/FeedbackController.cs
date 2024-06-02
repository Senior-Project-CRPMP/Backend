using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly DataContext _context;
        public FeedbackController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("feedbacks")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        {
            return await _context.Feedbacks.ToListAsync();
        }
        [HttpPost("feedbacks")]
        public async Task<ActionResult<Feedback>> CreateFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest("Feedback object is null");
            }

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFeedback), new { id = feedback.Id }, feedback);
        }
    }
}
