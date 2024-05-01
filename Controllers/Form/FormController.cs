using AutoMapper;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Interfaces.Form;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.Form>))]
        public IActionResult GetForms()
        {
            var forms = _mapper.Map<List<Dto.Form.FormDto>>(_formRepository.GetForms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("{id}/form")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Form.Form>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectForms(int id)
        {
            if (!_formRepository.ProjectFormExists(id))
                return NotFound();

            var forms = _mapper.Map<List<Dto.Form.FormDto>>(_formRepository.GetProjectForms(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("{id}", Name = "FormById")]
        [ProducesResponseType(200, Type = typeof(Models.Form.Form))]
        [ProducesResponseType(400)]
        public IActionResult GetForm(int id)
        {
            if (_formRepository.FormExists(id))
                return NotFound();

            var Form = _mapper.Map<Dto.Form.FormDto>(_formRepository.GetForm(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Form);
        }

        [HttpGet("form/count")]
        [ProducesResponseType(200)]
        public IActionResult GetFormCount()
        {
            var count = _formRepository.GetFormCount();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateForm([FromBody] Dto.Form.FormDto formToCreate)
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

        [HttpPut("{formId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateForm(int formId, [FromBody] Dto.Form.FormDto formToUpdate)
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

        [HttpDelete("{formId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteForm(int formId)
        {
            if (_formRepository.FormExists(formId))
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
