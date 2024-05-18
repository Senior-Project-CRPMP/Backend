using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormAnswerController : ControllerBase
    {
        private readonly IFormAnswerRepository _FormAnswerRepository;
        private readonly IMapper _mapper;

        public FormAnswerController(IFormAnswerRepository FormAnswerRepository, IMapper mapper)
        {
            _FormAnswerRepository = FormAnswerRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryFormAnswer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormAnswer>))]
        public IActionResult GetFormAnswers()
        {
            var FormAnswers = _mapper.Map<List<FormAnswerDto>>(_FormAnswerRepository.GetFormAnswers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(FormAnswers);
        }

        [HttpGet("SingleFormAnswer/{FormAnswerId}")]
        [ProducesResponseType(200, Type = typeof(FormAnswer))]
        [ProducesResponseType(400)]
        public IActionResult GetFormAnswer(int FormAnswerId)
        {
            if (!_FormAnswerRepository.FormAnswerExists(FormAnswerId))
                return NotFound();

            var FormAnswer = _mapper.Map<FormAnswerDto>(_FormAnswerRepository.GetFormAnswer(FormAnswerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(FormAnswer);
        }

        [HttpPost("CreateFormAnswer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormAnswer([FromBody] FormAnswerDto FormAnswerCreate)
        {
            if (FormAnswerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var FormAnswerMap = _mapper.Map<FormAnswer>(FormAnswerCreate);

            if (!_FormAnswerRepository.CreateFormAnswer(FormAnswerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateFormAnswer/{FormAnswerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormAnswer(int FormAnswerId, [FromBody] FormAnswerDto updatedFormAnswer)
        {
            if (updatedFormAnswer == null)
                return BadRequest(ModelState);

            if (FormAnswerId != updatedFormAnswer.Id)
                return BadRequest(ModelState);

            if (!_FormAnswerRepository.FormAnswerExists(FormAnswerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var FormAnswerMap = _mapper.Map<FormAnswer>(updatedFormAnswer);

            if (!_FormAnswerRepository.UpdateFormAnswer(FormAnswerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFormAnswer/{FormAnswerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormAnswer(int FormAnswerId)
        {
            if (!_FormAnswerRepository.FormAnswerExists(FormAnswerId))
                return NotFound();

            var FormAnswerToDelete = _FormAnswerRepository.GetFormAnswer(FormAnswerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_FormAnswerRepository.DeleteFormAnswer(FormAnswerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting FormAnswer");

            }

            return NoContent();



        }
    }
}
