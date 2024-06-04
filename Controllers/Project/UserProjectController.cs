using AutoMapper;
using Backend.Dto.Project;
using Backend.Interfaces.Project;
using Backend.Models.Project;
using Backend.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Backend.Dto.Account;

namespace Backend.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IMapper _mapper;

        public UserProjectController(IUserProjectRepository userProjectRepository, IMapper mapper)
        {
            _userProjectRepository = userProjectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUserProjects()
        {
            var userProjects = _mapper.Map<List<UserProjectDto>>(_userProjectRepository.GetUserProjects());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userProjects);
        }

        [HttpGet("{userProjectId}")]
        public IActionResult GetUserProject(int userProjectId)
        {
            if (!_userProjectRepository.UserProjectExists(userProjectId))
                return NotFound();

            var userProject = _mapper.Map<UserProjectDto>(_userProjectRepository.GetUserProject(userProjectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userProject);
        }

        [HttpGet("projectsByUserId/{userId}")]
        public IActionResult GetProjectsByUserId(string userId)
        {
            var projects = _mapper.Map<List<ProjectDto>>(_userProjectRepository.GetProjectsByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(projects);
        }

        [HttpGet("projectsByUserRole/{userId}/{role}")]
        public IActionResult GetProjectsByUserRole(string userId, string role)
        {
            var projects = _mapper.Map<List<ProjectDto>>(_userProjectRepository.GetProjectsByUserRole(userId, role));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(projects);
        }

        [HttpGet("usersByProjectId/{projectId}")]
        public IActionResult GetUsersByProjectId(int projectId)
        {
            var users = _mapper.Map<List<UserDto>>(_userProjectRepository.GetUsersByProjectId(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUserProject([FromBody] UserProjectDto userProjectCreate)
        {
            if (userProjectCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userProjectMap = _mapper.Map<UserProject>(userProjectCreate);

            if (!_userProjectRepository.CreateUserProject(userProjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{userProjectId}")]
        public IActionResult UpdateUserProject(int userProjectId, [FromBody] UserProjectDto updatedUserProject)
        {
            if (updatedUserProject == null)
                return BadRequest(ModelState);

            if (userProjectId != updatedUserProject.Id)
                return BadRequest(ModelState);

            if (!_userProjectRepository.UserProjectExists(userProjectId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userProjectMap = _mapper.Map<UserProject>(updatedUserProject);

            if (!_userProjectRepository.UpdateUserProject(userProjectMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the user project");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userProjectId}")]
        public IActionResult DeleteUserProject(int userProjectId)
        {
            if (!_userProjectRepository.UserProjectExists(userProjectId))
                return NotFound();

            var userProjectToDelete = _userProjectRepository.GetUserProject(userProjectId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userProjectRepository.DeleteUserProject(userProjectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user project");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
