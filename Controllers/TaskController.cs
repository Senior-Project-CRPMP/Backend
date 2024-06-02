using AutoMapper;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet("EveryTask")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Task>))]
        public IActionResult GetTasks()
        {
            var tasks = _mapper.Map<List<TaskDto>>(_taskRepository.GetTasks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpGet("ProjectTasks/{projectId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Task>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectTasks(int projectId)
        {
            if (!_taskRepository.ProjectTaskExists(projectId))
                return NotFound();

            var tasks = _mapper.Map<List<TaskDto>>(_taskRepository.GetProjectTasks(projectId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpGet("SingleTask/{taskId}")]
        [ProducesResponseType(200, Type = typeof(Models.Task))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(int taskId)
        {
            if (_taskRepository.TaskExists(taskId))
                return NotFound();

            var task = _mapper.Map<TaskDto>(_taskRepository.GetTask(taskId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("TaskCount")]
        [ProducesResponseType(200)]
        public IActionResult GetTaskCount()
        {
            var count = _taskRepository.GetTaskCount();
            return Ok(count);
        }

        [HttpPost("CreateTask")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] TaskDto taskToCreate)
        {
            if (taskToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskMap = _mapper.Map<Models.Task>(taskToCreate);

            if (!_taskRepository.CreateTask(taskMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("UpdateTask/{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] TaskDto taskToUpdate)
        {
            if (taskToUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (taskId != taskToUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_taskRepository.TaskExists(taskId))
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var taskMap = _mapper.Map<Models.Task>(taskToUpdate);

            if (!_taskRepository.UpdateTask(taskMap))
            {
                ModelState.AddModelError("", "Something Went Wrong Updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("DeleteTask/{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(int taskId)
        {
            if (_taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }

            var taskToDelete = _taskRepository.GetTask(taskId);

            if (!_taskRepository.DeleteTask(taskToDelete))
            {
                ModelState.AddModelError("", "Something Went Wrong Deleting");
            }

            return Ok("Successfully Deleted");
        }


    }
}
