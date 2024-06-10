using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.Form
{
    public class FormResponseRepository : IFormResponseRepository
    {
        private readonly DataContext _context;
        public FormResponseRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormResponse(FormResponse formResponse)
        {
            _context.Add(formResponse);
            return Save();
        }

        public bool DeleteFormResponse(FormResponse formResponse)
        {
            _context.Remove(formResponse);
            return Save();
        }

        public FormResponse GetFormResponse(int id)
        {
            return _context.FormResponses.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<FormResponse> GetFormResponses()
        {
            return _context.FormResponses.OrderBy(p => p.Id).ToList();
        }

        public int GetFormResponseCount()
        {
            return _context.FormResponses.Count();
        }

        public int GetFormResponseCountByFormId(int formId)
        {
            return _context.FormResponses.Count(fr => fr.FormId == formId);
        }

        public bool FormResponseExists(int id)
        {
            return _context.FormResponses.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormResponse(FormResponse formResponse)
        {
            _context.Update(formResponse);
            return Save();
        }
    }
}
