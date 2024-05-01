using AutoMapper;
using Backend.Dto;
using Backend.Dto.Form;
using Backend.Interfaces;
using Backend.Interfaces.Form;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public FormController(IFormRepository formRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryForm")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.Form>))]
        public IActionResult GetForms()
        {
            var forms = _mapper.Map<List<FormDto>>(_formRepository.GetForms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("ProjectForms/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.Form>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectForms(int formId)
        {
            if (!_formRepository.ProjectFormExists(formId))
                return NotFound();

            var forms = _mapper.Map<List<FormDto>>(_formRepository.GetProjectForms(formId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("SingleForm/{formId}")]
        [ProducesResponseType(200, Type = typeof(Models.Form.Form))]
        [ProducesResponseType(400)]
        public IActionResult GetForm(int formId)
        {
            if (!_formRepository.FormExists(formId))
                return NotFound();

            var form = _mapper.Map<FormDto>(_formRepository.GetForm(formId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(form);
        }

        [HttpGet("FormCount")]
        [ProducesResponseType(200)]
        public IActionResult GetFormCount()
        {
            var count = _formRepository.GetFormCount();
            return Ok(count);
        }

        [HttpPost("CreateForm")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateForm([FromBody] FormDto formToCreate)
        {
            if (formToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formMap = _mapper.Map<Models.Form.Form>(formToCreate);

            if (!_formRepository.CreateForm(formMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateForm/{formId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateForm(int formId, [FromBody] FormDto formToUpdate)
        {
            if (formToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (formId != formToUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_formRepository.FormExists(formId))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var formMap = _mapper.Map<Models.Form.Form>(formToUpdate);

            if (!_formRepository.UpdateForm(formMap))
            {
                ModelState.AddModelError("", "Something Went Wrong Updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("DeleteForm/{formId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteForm(int formId)
        {
            if (!_formRepository.FormExists(formId))
            {
                return NotFound();
            }

            var formToDelete = _formRepository.GetForm(formId);

            if (!_formRepository.DeleteForm(formToDelete))
            {
                ModelState.AddModelError("", "Something Went Wrong Deleting");
            }

            return Ok("Successfully Deleted");
        }
    }
}
