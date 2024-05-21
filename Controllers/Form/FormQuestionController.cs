using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormQuestionController : ControllerBase
    {
        private readonly IFormQuestionRepository _formQuestionRepository;
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormQuestionController(IFormQuestionRepository formQuestionRepository, IFormRepository formRepository, IMapper mapper)
        {
            _formQuestionRepository = formQuestionRepository;
            _formRepository = formRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryQuestion")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormQuestionDto>))]
        public IActionResult GetFormQuestions()
        {
            var questions = _mapper.Map<List<FormQuestionDto>>(_formQuestionRepository.GetFormQuestions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(questions);
        }

        [HttpGet("SingleQuestion/{questionId}")]
        [ProducesResponseType(200, Type = typeof(FormQuestionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFormQuestion(int questionId)
        {
            if (!_formQuestionRepository.FormQuestionExists(questionId))
                return NotFound();

            var question = _mapper.Map<FormQuestionDto>(_formQuestionRepository.GetFormQuestion(questionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(question);
        }

        [HttpGet("QuestionCount")]
        [ProducesResponseType(200)]
        public IActionResult GetFormQuestionCount()
        {
            var count = _formQuestionRepository.GetFormQuestionCount();
            return Ok(count);
        }

        [HttpPost("CreateQuestion")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormQuestion([FromBody] FormQuestionDto questionToCreate)
        {
            if (questionToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionMap = _mapper.Map<FormQuestion>(questionToCreate);

            if (!_formQuestionRepository.CreateFormQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { id = questionMap.Id });
        }

        [HttpPut("UpdateQuestion/{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormQuestion(int questionId, [FromBody] FormQuestionDto questionToUpdate)
        {
            if (questionToUpdate == null)
                return BadRequest(ModelState);

            if (questionId != questionToUpdate.Id)
                return BadRequest(ModelState);

            if (!_formQuestionRepository.FormQuestionExists(questionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionMap = _mapper.Map<FormQuestion>(questionToUpdate);

            if (!_formQuestionRepository.UpdateFormQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong Updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("DeleteQuestion/{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormQuestion(int questionId)
        {
            if (!_formQuestionRepository.FormQuestionExists(questionId))
                return NotFound();

            var questionToDelete = _formQuestionRepository.GetFormQuestion(questionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formQuestionRepository.DeleteFormQuestion(questionToDelete))
            {
                ModelState.AddModelError("", "Something Went Wrong Deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Deleted");
        }
    }
}
