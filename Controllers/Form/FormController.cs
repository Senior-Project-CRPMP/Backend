﻿using AutoMapper;
using Backend.Dto;
using Backend.Dto.Form;
using Backend.Interfaces.Form;
using Backend.Interfaces.Project;
using Backend.Models;
using Backend.Models.Form;
using Backend.Repository.Form;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers.Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IFormResponseRepository _formResponseRepository;
        private readonly IMapper _mapper;

        public FormController(IFormRepository formRepository, IProjectRepository projectRepository, IFormResponseRepository formResponseRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _projectRepository = projectRepository;
            _formResponseRepository = formResponseRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryForm")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormDto>))]
        public IActionResult GetForms()
        {
            var forms = _mapper.Map<List<FormDto>>(_formRepository.GetForms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("ProjectForms/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectForms(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var forms = _mapper.Map<List<FormDto>>(_formRepository.GetProjectForms(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(forms);
        }

        [HttpGet("SingleForm/{formId}")]
        [ProducesResponseType(200, Type = typeof(FormDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

        [HttpGet("FormCountByProject/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetFormCountByProject(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var count = _formRepository.GetProjectForms(projectId).Count;
            return Ok(count);
        }

        [HttpPost("CreateForm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateForm([FromBody] FormDto formToCreate)
        {
            if (formToCreate == null)
                return BadRequest(ModelState);

            var form = _formRepository.GetForms()
                .Where(p => p.Title.Trim().ToUpper() == formToCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (form != null)
            {
                ModelState.AddModelError("", "Form already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formMap = _mapper.Map<Models.Form.Form>(formToCreate);

            if (!_formRepository.CreateForm(formMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new { id = formMap.Id });
        }

        [HttpPut("UpdateForm/{formId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateForm(int formId, [FromBody] FormDto formToUpdate)
        {
            if (formToUpdate == null)
                return BadRequest(ModelState);

            if (formId != formToUpdate.Id)
                return BadRequest(ModelState);

            if (!_formRepository.FormExists(formId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formMap = _mapper.Map<Models.Form.Form>(formToUpdate);

            if (!_formRepository.UpdateForm(formMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
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
                return NotFound();

            var formToDelete = _formRepository.GetForm(formId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_formRepository.DeleteForm(formToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Deleted");
        }

        [HttpGet("FormsWithResponses")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormDto>))]
        public IActionResult GetFormsWithResponses()
        {
            var forms = _formRepository.GetForms();
            foreach (var form in forms)
            {
                form.FormResponses = _formResponseRepository.GetFormResponsesByFormId(form.Id);
            }
            var formsDto = _mapper.Map<List<FormDto>>(forms);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formsDto);
        }

        [HttpGet("ProjectFormsWithResponses/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormWithResponsesDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectFormsWithResponses(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var forms = _formRepository.GetFormsWithResponsesByProjectId(projectId);
            var formDtos = _mapper.Map<List<FormWithResponsesDto>>(forms);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formDtos);
        }

        [HttpGet("ProjectFormsWithResponsesOnly/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FormDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectFormsWithResponsesOnly(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var forms = _formRepository.GetFormsByProjectIdWithResponses(projectId);
            var formDtos = _mapper.Map<List<FormDto>>(forms);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(formDtos);
        }
    }
}
