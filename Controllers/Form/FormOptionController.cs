using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormOptionController : ControllerBase
    {
        private readonly IFormOptionRepository _formOptionRepository;
        private readonly IMapper _mapper;

        public FormOptionController(IFormOptionRepository formOptionRepository, IMapper mapper)
        {
            _formOptionRepository = formOptionRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryOption")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormOptionDto>))]
        public IActionResult GetFormOptions()
        {
            var options = _mapper.Map<List<FormOptionDto>>(_formOptionRepository.GetFormOptions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(options);
        }

        [HttpGet("SingleOption/{optionId}")]
        [ProducesResponseType(200, Type = typeof(FormOptionDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFormOption(int optionId)
        {
            if (!_formOptionRepository.FormOptionExists(optionId))
                return NotFound();

            var formOption = _mapper.Map<FormOptionDto>(_formOptionRepository.GetFormOption(optionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formOption);
        }

        [HttpPost("CreateOption")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateFormOption([FromBody] FormOptionDto optionCreate)
        {
            if (optionCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var optionMap = _mapper.Map<FormOption>(optionCreate);

            if (!_formOptionRepository.CreateFormOption(optionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateOption/{optionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFormOption(int optionId, [FromBody] FormOptionDto updatedOption)
        {
            if (updatedOption == null)
                return BadRequest(ModelState);

            if (optionId != updatedOption.Id)
                return BadRequest(ModelState);

            if (!_formOptionRepository.FormOptionExists(optionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var optionMap = _mapper.Map<FormOption>(updatedOption);

            if (!_formOptionRepository.UpdateFormOption(optionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the form option");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOption/{optionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFormOption(int optionId)
        {
            if (!_formOptionRepository.FormOptionExists(optionId))
                return NotFound();

            var optionToDelete = _formOptionRepository.GetFormOption(optionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formOptionRepository.DeleteFormOption(optionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the form option");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
