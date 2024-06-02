using Backend.Models.FileUpload;

namespace Backend.Interfaces.FileUpload
{
    public interface IProfilePicUploadRepository
    {
        Task<ProfilePicUpload> GetFileByIdAsync(int id);
        Task<IEnumerable<ProfilePicUpload>> GetAllFilesAsync();
        Task<ProfilePicUpload> AddFileAsync(ProfilePicUpload profilePicUpload);
        Task<bool> DeleteFileAsync(int id);
    }
}
