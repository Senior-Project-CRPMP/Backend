using Backend.Models.FileUpload;
using Backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.Controllers.FileUploads
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadController(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }


        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadById(int id)
        {
            var fileUpload = await _fileUploadRepository.GetFileByIdAsync(id);

            if (fileUpload == null)
            {
                return NotFound("File not found");
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileUpload.FilePath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("File does not exist");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileContentResult = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = fileUpload.FileName,
            };

            return fileContentResult;
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileUploadRepository.GetAllFilesAsync();
            return Ok(files);
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Invalid File");
            }

            var folderName = Path.Combine("Resources", "ProjectFiles");
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

            var fileUpload = new Models.FileUpload.FileUpload
            {
                FilePath = dbPath,
                FileName = fileName,
                Name = model.Name,
                Description = model.Description
            };

            var createdFile = await _fileUploadRepository.AddFileAsync(fileUpload);

            return Ok(new { id = createdFile.Id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var success = await _fileUploadRepository.DeleteFileAsync(id);
            if (!success)
            {
                return NotFound("File not found");
            }

            return NoContent();
        }
    }
}
