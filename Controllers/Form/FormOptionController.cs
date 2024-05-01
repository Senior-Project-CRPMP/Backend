using AutoMapper;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormOption>))]
        public IActionResult GetFormOptions()
        {
            var projects = _mapper.Map<List<FormOptionDto>>(_formOptionRepository.GetFormOptions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(projects);
        }

        [HttpGet("SingleOption/{optionId}")]
        [ProducesResponseType(200, Type = typeof(FormOption))]
        [ProducesResponseType(400)]
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
        public IActionResult CreateProject([FromBody] FormOptionDto optionCreate)
        {
            if (optionCreate == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectMap = _mapper.Map<FormOption>(optionCreate);

            if (!_formOptionRepository.CreateFormOption(projectMap))
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
        public IActionResult UpdateProject(int optionId, [FromBody] FormOptionDto updatedProject)
        {
            if (updatedProject == null)
                return BadRequest(ModelState);

            if (optionId != updatedProject.Id)
                return BadRequest(ModelState);

            if (!_formOptionRepository.FormOptionExists(optionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<FormOption>(updatedProject);

            if (!_formOptionRepository.UpdateFormOption(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOption/{optionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int optionId)
        {
            if (!_formOptionRepository.FormOptionExists(optionId))
                return NotFound();

            var projectToDelete = _formOptionRepository.GetFormOption(optionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formOptionRepository.DeleteFormOption(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting formOption");

            }

            return NoContent();



        }
    }
}
