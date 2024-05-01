using AutoMapper;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleModel>))]
        public IActionResult GetRoles()
        {
            var roles = _mapper.Map<List<RoleDto>>(_roleRepository.GetRoles());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(roles);
        }

        [HttpGet("{roleId}")]
        [ProducesResponseType(200, Type = typeof(RoleModel))]
        [ProducesResponseType(400)]
        public IActionResult Getrole(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            var role = _mapper.Map<RoleDto>(_roleRepository.GetRole(roleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(role);
        }

        [HttpGet("{roleName}")]
        [ProducesResponseType(200, Type = typeof(RoleModel))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(string roleName)
        {
            if (!_roleRepository.RoleExists(roleName))
                return NotFound();

            var role = _roleRepository.GetRole(roleName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateRole([FromBody] RoleDto roleCreate)
        {
            if (roleCreate == null)
                return BadRequest(ModelState);

            var role = _roleRepository.GetRoles()
                .Where(p => p.Name.Trim().ToUpper() == roleCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (role != null)
            {
                ModelState.AddModelError("", "role already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleMap = _mapper.Map<RoleModel>(roleCreate);

            if (!_roleRepository.CreateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(int roleId, [FromBody] RoleDto updatedRole)
        {
            if (updatedRole == null)
                return BadRequest(ModelState);

            if (roleId != updatedRole.Id)
                return BadRequest(ModelState);

            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var roleMap = _mapper.Map<RoleModel>(updatedRole);

            if (!_roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRole(int roleId)
        {
            if (!_roleRepository.RoleExists(roleId))
                return NotFound();

            var roleToDelete = _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roleRepository.DeleteRole(roleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting role");

            }

            return NoContent();



        }
    }
}
