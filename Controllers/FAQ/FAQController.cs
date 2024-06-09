using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers.FAQ
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly DataContext _context;

        public FAQController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.FAQ.FAQ>> GetAll()
        {
            return _context.FAQs.ToList();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Models.FAQ.FAQ>> Search([FromQuery] string query)
        {
            return _context.FAQs.Where(f => f.Question.Contains(query)).ToList();
        }

        [HttpPost]
        public ActionResult<Models.FAQ.FAQ> Create(Models.FAQ.FAQ faq)
        {
            _context.FAQs.Add(faq);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAll), new { id = faq.Id }, faq);
        }
    }
}
