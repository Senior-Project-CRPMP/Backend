using AutoMapper;
using Backend.Dto;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoRepository userInfoRepository, IMapper mapper)
        {
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserInfoModel>))]
        public IActionResult GetUserInfos()
        {
            var userInfos = _mapper.Map<List<UserInfoDto>>(_userInfoRepository.GetUserInfos());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userInfos);
        }

        [HttpGet("{userInfoId}")]
        [ProducesResponseType(200, Type = typeof(UserInfoModel))]
        [ProducesResponseType(400)]
        public IActionResult GetUserInfo(int userInfoId)
        { 

            var userInfo = _mapper.Map<UserInfoDto>(_userInfoRepository.GetUserInfo(userInfoId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userInfo);
        }

        [HttpGet("{userInfoName}")]
        [ProducesResponseType(200, Type = typeof(UserInfoModel))]
        [ProducesResponseType(400)]
        public IActionResult GetUserInfo(string userInfoName)
        {

            var userInfo = _userInfoRepository.GetUserInfo(userInfoName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userInfo);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserInfo([FromBody] UserInfoDto userInfoCreate)
        {
            if (userInfoCreate == null)
                return BadRequest(ModelState);

            var userInfo = _userInfoRepository.GetUserInfos()
                .Where(p => p.FirstName.Trim().ToUpper() == userInfoCreate.FirstName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (userInfo != null)
            {
                ModelState.AddModelError("", "UserInfo already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userInfoMap = _mapper.Map<UserInfoModel>(userInfoCreate);

            if (!_userInfoRepository.CreateUserInfo(userInfoMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }

        [HttpPut("{userInfoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserInfo(int userInfoId, [FromBody] UserInfoDto updatedUserInfo)
        {
            if (updatedUserInfo == null)
                return BadRequest(ModelState);

            if (userInfoId != updatedUserInfo.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var userInfoMap = _mapper.Map<UserInfoModel>(updatedUserInfo);

            if (!_userInfoRepository.UpdateUserInfo(userInfoMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userInfoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserInfo(int userInfoId)
        {
            var userInfoToDelete = _userInfoRepository.GetUserInfo(userInfoId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userInfoRepository.DeleteUserInfo(userInfoToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting userInfo");

            }

            return NoContent();



        }
    }
}
