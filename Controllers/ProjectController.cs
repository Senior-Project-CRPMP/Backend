using AutoMapper;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projects = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProject(int projectId)
        {
            if(!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var project = _mapper.Map<ProjectDto>(_projectRepository.GetProject(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpGet("{projectTitle}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProject(string projectTitle)
        {
            if (!_projectRepository.ProjectExists(projectTitle))
                return NotFound();

            var project = _projectRepository.GetProject(projectTitle);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProject([FromBody] ProjectDto projectCreate)
        {
            if (projectCreate == null)
                return BadRequest(ModelState);

            var project = _projectRepository.GetProjects()
                .Where(p => p.Title.Trim().ToUpper() == projectCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (project != null)
            {
                ModelState.AddModelError("", "Project already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectMap = _mapper.Map<Project>(projectCreate);

            if (!_projectRepository.CreateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody]ProjectDto updatedProject) 
        {
            if (updatedProject == null)
                return BadRequest(ModelState);

            if(projectId != updatedProject.Id)
                return BadRequest(ModelState);

            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var projectMap = _mapper.Map<Project>(updatedProject);

            if (!_projectRepository.UpdateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            var projectToDelete = _projectRepository.GetProject(projectId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_projectRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting project");

            }

            return NoContent(); 


            
        }
    }
}
