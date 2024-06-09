using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormAnswerController : ControllerBase
    {
        private readonly IFormAnswerRepository _formAnswerRepository;
        private readonly IMapper _mapper;

        public FormAnswerController(IFormAnswerRepository formAnswerRepository, IMapper mapper)
        {
            _formAnswerRepository = formAnswerRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryFormAnswer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormAnswerDto>))]
        public IActionResult GetFormAnswers()
        {
            var formAnswers = _mapper.Map<List<FormAnswerDto>>(_formAnswerRepository.GetFormAnswers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(formAnswers);
        }

        [HttpGet("SingleFormAnswer/{formAnswerId}")]
        [ProducesResponseType(200, Type = typeof(FormAnswerDto))]
        [ProducesResponseType(400)]
        public IActionResult GetFormAnswer(int formAnswerId)
        {
            if (!_formAnswerRepository.FormAnswerExists(formAnswerId))
                return NotFound();

            var formAnswer = _mapper.Map<FormAnswerDto>(_formAnswerRepository.GetFormAnswer(formAnswerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formAnswer);
        }

        [HttpPost("CreateFormAnswer")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormAnswer([FromBody] FormAnswerDto formAnswerCreate)
        {
            if (formAnswerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formAnswerMap = _mapper.Map<FormAnswer>(formAnswerCreate);

            if (!_formAnswerRepository.CreateFormAnswer(formAnswerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { formAnswerId = formAnswerMap.Id });
        }

        [HttpPut("UpdateFormAnswer/{formAnswerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormAnswer(int formAnswerId, [FromBody] FormAnswerDto updatedFormAnswer)
        {
            if (updatedFormAnswer == null)
                return BadRequest(ModelState);

            if (formAnswerId != updatedFormAnswer.Id)
                return BadRequest(ModelState);

            if (!_formAnswerRepository.FormAnswerExists(formAnswerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var formAnswerMap = _mapper.Map<FormAnswer>(updatedFormAnswer);

            if (!_formAnswerRepository.UpdateFormAnswer(formAnswerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFormAnswer/{formAnswerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormAnswer(int formAnswerId)
        {
            if (!_formAnswerRepository.FormAnswerExists(formAnswerId))
                return NotFound();

            var formAnswerToDelete = _formAnswerRepository.GetFormAnswer(formAnswerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formAnswerRepository.DeleteFormAnswer(formAnswerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
