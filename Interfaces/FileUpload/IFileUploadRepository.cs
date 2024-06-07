using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.FileUpload;

namespace Backend.Repositories
{
    public interface IFileUploadRepository
    {
        Task<FileUpload> GetFileByIdAsync(int id);
        Task<IEnumerable<FileUpload>> GetAllFilesAsync();
        Task<IEnumerable<FileUpload>> GetFilesByProjectIdAsync(int projectId);
        FileUpload GetFileUpload(int id);
        Task<FileUpload> AddFileAsync(FileUpload fileUpload);
        Task<bool> DeleteFileAsync(int id);
        bool FileUploadExists(int id);
    }
}
