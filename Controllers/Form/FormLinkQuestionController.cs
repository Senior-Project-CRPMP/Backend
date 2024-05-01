using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormLinkQuestionController : ControllerBase
    {
        private readonly IFormLinkQuestionRepository _formQuestionRepository;
        private readonly IMapper _mapper;

        public FormLinkQuestionController(IFormLinkQuestionRepository formQuestionRepository, IMapper mapper)
        {
            _formQuestionRepository = formQuestionRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryFQL")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormLinkQuestion>))]
        public IActionResult GetFormLinkQuestions()
        {
            var formQuestions = _mapper.Map<List<FormLinkQuestionDto>>(_formQuestionRepository.GetFormLinkQuestions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(formQuestions);
        }

        [HttpGet("SingleFQL/{formQuestionId}")]
        [ProducesResponseType(200, Type = typeof(FormLinkQuestion))]
        [ProducesResponseType(400)]
        public IActionResult GetFormLinkQuestion(int formQuestionId)
        {
            if (!_formQuestionRepository.FormLinkQuestionExists(formQuestionId))
                return NotFound();

            var formQuestion = _mapper.Map<FormLinkQuestionDto>(_formQuestionRepository.GetFormLinkQuestion(formQuestionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formQuestion);
        }

        [HttpPost("CreateFQL")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormLinkQuestion([FromBody] FormLinkQuestionDto formQuestionCreate)
        {

            var formQuestionMap = _mapper.Map<FormLinkQuestion>(formQuestionCreate);

            if (!_formQuestionRepository.CreateFormLinkQuestion(formQuestionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateFQL/{formQuestionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormLinkQuestion(int formQuestionId, [FromBody] FormLinkQuestionDto updatedFormLinkQuestion)
        {
            if (updatedFormLinkQuestion == null)
                return BadRequest(ModelState);

            if (formQuestionId != updatedFormLinkQuestion.Id)
                return BadRequest(ModelState);

            if (!_formQuestionRepository.FormLinkQuestionExists(formQuestionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var formQuestionMap = _mapper.Map<FormLinkQuestion>(updatedFormLinkQuestion);

            if (!_formQuestionRepository.UpdateFormLinkQuestion(formQuestionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFQL/{formQuestionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormLinkQuestion(int formQuestionId)
        {
            if (!_formQuestionRepository.FormLinkQuestionExists(formQuestionId))
                return NotFound();

            var formQuestionToDelete = _formQuestionRepository.GetFormLinkQuestion(formQuestionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formQuestionRepository.DeleteFormLinkQuestion(formQuestionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting formQuestion");

            }

            return NoContent();



        }
    }
}
