using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models.FileUpload;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly DataContext _context;

        public FileUploadRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<FileUpload> AddFileAsync(FileUpload fileUpload)
        {
            _context.FileUploads.Add(fileUpload);
            await _context.SaveChangesAsync();
            return fileUpload;
        }

        public async Task<IEnumerable<FileUpload>> GetFilesByProjectIdAsync(int projectId)
        {
            return await _context.FileUploads
                                 .Where(f => f.ProjectId == projectId)
                                 .ToListAsync();
        }

        public async Task<FileUpload> GetFileByIdAsync(int id)
        {
            return await _context.FileUploads.FindAsync(id);
        }

        public async Task<IEnumerable<FileUpload>> GetAllFilesAsync()
        {
            return await _context.FileUploads.ToListAsync();
        }

        public async Task<bool> DeleteFileAsync(int id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
            {
                return false;
            }

            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileUpload.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            return true;
        }

        public FileUpload GetFileUpload(int id)
        {
            return _context.FileUploads.Where(f => f.Id == id).FirstOrDefault();
        }

        public bool FileUploadExists(int id)
        {
            return _context.FileUploads.Any(f => f.Id == id);
        }
    }
}
