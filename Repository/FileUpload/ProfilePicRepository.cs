using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Interfaces.FileUpload;
using Backend.Models.FileUpload;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ProfilePicUploadRepository : IProfilePicUploadRepository
    {
        private readonly DataContext _context;

        public ProfilePicUploadRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProfilePicUpload> AddFileAsync(ProfilePicUpload profilePicUpload)
        {
            _context.ProfilePicUploads.Add(profilePicUpload);
            await _context.SaveChangesAsync();
            return profilePicUpload;
        }

        public async Task<ProfilePicUpload> GetFileByIdAsync(int id)
        {
            return await _context.ProfilePicUploads.FindAsync(id);
        }

        public async Task<IEnumerable<ProfilePicUpload>> GetAllFilesAsync()
        {
            return await _context.ProfilePicUploads.ToListAsync();
        }

        public async Task<bool> DeleteFileAsync(int id)
        {
            var profilePicUpload = await _context.ProfilePicUploads.FindAsync(id);
            if (profilePicUpload == null)
            {
                return false;
            }

            _context.ProfilePicUploads.Remove(profilePicUpload);
            await _context.SaveChangesAsync();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), profilePicUpload.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            return true;
        }
    }
}
