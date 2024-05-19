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
    public class FormFileStorageController : ControllerBase
    {
        private readonly IFormFileStorageRepository _formFileStorageRepository;
        private readonly IMapper _mapper;

        public FormFileStorageController(IFormFileStorageRepository formFileStorageRepository, IMapper mapper)
        {
            _formFileStorageRepository = formFileStorageRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryFormFileStorage")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormFileStorageDto>))]
        public IActionResult GetFormFileStorages()
        {
            var formFileStorages = _mapper.Map<List<FormFileStorageDto>>(_formFileStorageRepository.GetFormFileStorages());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(formFileStorages);
        }

        [HttpGet("SingleFormFileStorage/{formFileStorageId}")]
        [ProducesResponseType(200, Type = typeof(FormFileStorageDto))]
        [ProducesResponseType(400)]
        public IActionResult GetFormFileStorage(int formFileStorageId)
        {
            if (!_formFileStorageRepository.FormFileStorageExists(formFileStorageId))
                return NotFound();

            var formFileStorage = _mapper.Map<FormFileStorageDto>(_formFileStorageRepository.GetFormFileStorage(formFileStorageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formFileStorage);
        }

        [HttpPost("CreateFormFileStorage")]
        [ProducesResponseType(201)] // Created status code
        [ProducesResponseType(400)]
        public IActionResult CreateFormFileStorage([FromBody] FormFileStorageDto formFileStorageCreate)
        {
            if (formFileStorageCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formFileStorageMap = _mapper.Map<FormFileStorage>(formFileStorageCreate);

            if (!_formFileStorageRepository.CreateFormFileStorage(formFileStorageMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction("GetFormFileStorage", new { formFileStorageId = formFileStorageMap.Id }, "Successfully Created");
        }

        [HttpPut("UpdateFormFileStorage/{formFileStorageId}")]
        [ProducesResponseType(204)] // No Content status code
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormFileStorage(int formFileStorageId, [FromBody] FormFileStorageDto updatedFormFileStorage)
        {
            if (updatedFormFileStorage == null)
                return BadRequest(ModelState);

            if (formFileStorageId != updatedFormFileStorage.Id)
                return BadRequest(ModelState);

            if (!_formFileStorageRepository.FormFileStorageExists(formFileStorageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var formFileStorageMap = _mapper.Map<FormFileStorage>(updatedFormFileStorage);

            if (!_formFileStorageRepository.UpdateFormFileStorage(formFileStorageMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFormFileStorage/{formFileStorageId}")]
        [ProducesResponseType(204)] // No Content status code
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormFileStorage(int formFileStorageId)
        {
            if (!_formFileStorageRepository.FormFileStorageExists(formFileStorageId))
                return NotFound();

            var formFileStorageToDelete = _formFileStorageRepository.GetFormFileStorage(formFileStorageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formFileStorageRepository.DeleteFormFileStorage(formFileStorageToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
