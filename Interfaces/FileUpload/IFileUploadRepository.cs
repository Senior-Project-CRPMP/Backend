using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.FileUpload;

namespace Backend.Repositories
{
    public interface IFileUploadRepository
    {
        Task<FileUpload> GetFileByIdAsync(int id);
        Task<IEnumerable<FileUpload>> GetAllFilesAsync();
        Task<FileUpload> AddFileAsync(FileUpload fileUpload);
        Task<bool> DeleteFileAsync(int id);
    }
}
