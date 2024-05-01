using AutoMapper;
using Backend.Dto;
using Backend.Interfaces.Form;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Form
{
    // GetFormQuestion and DeleteFormQuestion endpoints dont work.

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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.FormQuestion>))]
        public IActionResult GetFormQuestions()
        {
            var forms = _mapper.Map<List<Dto.Form.FormQuestionDto>>(_formQuestionRepository.GetFormQuestions());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("{questionId}/formquestion")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.FormQuestion>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFormQuestion(int questionId)
        {
            if (!_formQuestionRepository.FormQuestionExists(questionId))
                return NotFound();

            var question = _mapper.Map<ProjectDto>(_formQuestionRepository.GetFormQuestion(questionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(question);
        }

        [HttpGet("formQuestion/count")]
        [ProducesResponseType(200)]
        public IActionResult GetFormQuestionCount()
        {
            var count = _formQuestionRepository.GetFormQuestionCount();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormQuestion([FromBody] Dto.Form.FormQuestionDto questionToCreate)
        {
            if (questionToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionMap = _mapper.Map<Models.Form.FormQuestion>(questionToCreate);

            if (!_formQuestionRepository.CreateFormQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormQuestion(int questionId, [FromBody] Dto.Form.FormQuestionDto questionToUpdate)
        {
            if (questionToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (questionId != questionToUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_formQuestionRepository.FormQuestionExists(questionId))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var questionMap = _mapper.Map<Models.Form.FormQuestion>(questionToUpdate);

            if (!_formQuestionRepository.UpdateFormQuestion(questionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong Updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{questionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteForm(int questionId)
        {
            if (_formQuestionRepository.FormQuestionExists(questionId))
            {
                return NotFound();
            }

            var questionToDelete = _formQuestionRepository.GetFormQuestion(questionId);

            if (!_formQuestionRepository.DeleteFormQuestion(questionToDelete))
            {
                ModelState.AddModelError("", "Something Went Wrong Deleting");
            }

            return Ok("Successfully Deleted");
        }
    }
}
