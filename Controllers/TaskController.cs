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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskModel>))]
        public IActionResult GetTasks()
        {
            var tasks = _mapper.Map<List<TaskDto>>(_taskRepository.GetTasks());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpGet("{id}/project")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskModel>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetProjectTasks(int id)
        {
            if (!_taskRepository.ProjectTaskExists(id))
                return NotFound();

            var tasks = _mapper.Map<List<TaskDto>>(_taskRepository.GetProjectTasks(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        [ProducesResponseType(200, Type = typeof(TaskModel))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(int id)
        {
            if (_taskRepository.TaskExists(id))
                return NotFound();

            var task = _mapper.Map<TaskDto>(_taskRepository.GetTask(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }

        [HttpGet("count")]
        [ProducesResponseType(200)]
        public IActionResult GetTaskCount()
        {
            var count = _taskRepository.GetTaskCount();
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] TaskDto taskToCreate)
        {
            if (taskToCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskMap = _mapper.Map<TaskModel>(taskToCreate);

            if (!_taskRepository.CreateTask(taskMap))
            {
                ModelState.AddModelError("", "Something Went Wrong While Saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{taskId}")]
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

            var taskMap = _mapper.Map<TaskModel>(taskToUpdate);

            if (!_taskRepository.UpdateTask(taskMap))
            {
                ModelState.AddModelError("", "Something Went Wrong Updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }

        [HttpDelete("{taskId}")]
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
