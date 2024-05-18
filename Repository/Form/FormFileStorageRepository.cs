using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;

namespace Backend.Repository.Form
{
    public class FormFileStorageRepository : IFormFileStorageRepository
    {
        private readonly DataContext _context;
        public FormFileStorageRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormFileStorage(FormFileStorage formFileStorage)
        {
            _context.Add(formFileStorage);
            return Save();
        }

        public bool DeleteFormFileStorage(FormFileStorage formFileStorage)
        {
            _context.Remove(formFileStorage);
            return Save();
        }

        public FormFileStorage GetFormFileStorage(int id)
        {
            return _context.FormFileStorages.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<FormFileStorage> GetFormFileStorages()
        {
            return _context.FormFileStorages.OrderBy(p => p.Id).ToList();
        }

        public bool FormFileStorageExists(int id)
        {
            return _context.FormFileStorages.Any(p => p.Id == id);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormFileStorage(FormFileStorage formFileStorage)
        {
            _context.Update(formFileStorage);
            return Save();
        }
    }
}
