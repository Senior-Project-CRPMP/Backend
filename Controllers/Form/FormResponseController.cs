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
    public class FormResponseController : ControllerBase
    {
        private readonly IFormResponseRepository _formResponseRepository;
        private readonly IMapper _mapper;

        public FormResponseController(IFormResponseRepository formResponseRepository, IMapper mapper)
        {
            _formResponseRepository = formResponseRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryFormResponse")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormResponseDto>))]
        public IActionResult GetFormResponses()
        {
            var formResponses = _mapper.Map<List<FormResponseDto>>(_formResponseRepository.GetFormResponses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(formResponses);
        }

        [HttpGet("SingleFormResponse/{formResponseId}")]
        [ProducesResponseType(200, Type = typeof(FormResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFormResponse(int formResponseId)
        {
            if (!_formResponseRepository.FormResponseExists(formResponseId))
                return NotFound();

            var formResponse = _mapper.Map<FormResponseDto>(_formResponseRepository.GetFormResponse(formResponseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formResponse);
        }

        [HttpGet("TotalFormResponseCount")]
        [ProducesResponseType(200)]
        public IActionResult GetTotalFormResponseCount()
        {
            var count = _formResponseRepository.GetFormResponseCount();
            return Ok(count);
        }

        [HttpGet("FormResponseCountByFormId/{formId}")]
        [ProducesResponseType(200)]
        public IActionResult GetFormResponseCountByFormId(int formId)
        {
            var count = _formResponseRepository.GetFormResponseCountByFormId(formId);
            return Ok(count);
        }

        [HttpPost("CreateFormResponse")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormResponse([FromBody] FormResponseDto formResponseCreate)
        {
            if (formResponseCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formResponseMap = _mapper.Map<FormResponse>(formResponseCreate);

            if (!_formResponseRepository.CreateFormResponse(formResponseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { formResponseId = formResponseMap.Id });
        }

        [HttpPut("UpdateFormResponse/{formResponseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormResponse(int formResponseId, [FromBody] FormResponseDto updatedFormResponse)
        {
            if (updatedFormResponse == null)
                return BadRequest(ModelState);

            if (formResponseId != updatedFormResponse.Id)
                return BadRequest(ModelState);

            if (!_formResponseRepository.FormResponseExists(formResponseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var formResponseMap = _mapper.Map<FormResponse>(updatedFormResponse);

            if (!_formResponseRepository.UpdateFormResponse(formResponseMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFormResponse/{formResponseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormResponse(int formResponseId)
        {
            if (!_formResponseRepository.FormResponseExists(formResponseId))
                return NotFound();

            var formResponseToDelete = _formResponseRepository.GetFormResponse(formResponseId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formResponseRepository.DeleteFormResponse(formResponseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
