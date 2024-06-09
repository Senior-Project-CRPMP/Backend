using Backend.Data;
using Backend.Models.FAQ;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers.FAQ
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly DataContext _context;

        public QuestionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetAll()
        {
            return _context.Questions.ToList();
        }

        [HttpPost]
        public ActionResult<Question> Create(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAll), new { id = question.Id }, question);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var question = _context.Questions.Find(id);

            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
