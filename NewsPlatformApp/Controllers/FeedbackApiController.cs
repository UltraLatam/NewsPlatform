using Microsoft.AspNetCore.Mvc;
using NewsPlatformApp.Data;
using NewsPlatformApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NewsPlatformApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public FeedbackApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // POST: /api/feedbackapi
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] Feedback feedback)
        {
            if (await _db.Feedbacks.AnyAsync(f => f.PostId == feedback.PostId))
                return BadRequest("Ya existe feedback para este post.");

            feedback.Fecha = DateTime.UtcNow;
            _db.Feedbacks.Add(feedback);
            await _db.SaveChangesAsync();

            return Ok(feedback);
        }

        // GET: /api/feedbackapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _db.Feedbacks.OrderByDescending(f => f.Fecha).ToListAsync();
        }
    }
}
