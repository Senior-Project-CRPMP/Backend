using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;

namespace Backend.Repository
{
    public class FormOptionRepository : IFormOptionRepository
    {
        private readonly DataContext _context;
        public FormOptionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormOption(FormOption formOption)
        {
            _context.Add(formOption);
            return Save();
        }

        public bool DeleteFormOption(FormOption formOption)
        {
            _context.Remove(formOption);
            return Save();
        }

        public FormOption GetFormOption(int id)
        {
            return _context.FormOptions.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<FormOption> GetFormOptions()
        {
            return _context.FormOptions.OrderBy(p => p.Id).ToList();
        }

        public bool FormOptionExists(int id)
        {
            return _context.FormOptions.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormOption(FormOption formOption)
        {
            _context.Update(formOption);
            return Save();
        }
    }
}


