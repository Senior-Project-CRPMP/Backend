using Backend.Dto.FileUpload;
using Backend.Interfaces.FileUpload;
using Backend.Models.FileUpload;
using Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers.FileUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicUploadController : ControllerBase
    {
        private readonly IProfilePicUploadRepository _profilePicUploadRepository;

        public ProfilePicUploadController(IProfilePicUploadRepository profilePicUploadRepository)
        {
            _profilePicUploadRepository = profilePicUploadRepository;
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _profilePicUploadRepository.GetAllFilesAsync();
            return Ok(files);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> DownloadById(int id)
        {
            var profilePic = await _profilePicUploadRepository.GetFileByIdAsync(id);

            if (profilePic == null)
            {
                return NotFound("File not found");
            }

           
            return Ok(profilePic);
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] ProfilePicUploadDto model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Invalid File");
            }

            var folderName = Path.Combine("Resources", "ProfilePics");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = model.File.FileName;
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                return BadRequest("File already Exists");
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            var profilePicUpload = new ProfilePicUpload
            {
                FilePath = dbPath,
                UserId = model.UserId
            };

            var createdFile = await _profilePicUploadRepository.AddFileAsync(profilePicUpload);

            return Ok(new { id = createdFile.Id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var success = await _profilePicUploadRepository.DeleteFileAsync(id);
            if (!success)
            {
                return NotFound("File not found");
            }

            return NoContent();
        }
    }
}
