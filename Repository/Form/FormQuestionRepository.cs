using Backend.Data;
using Backend.Interfaces;
using Backend.Interfaces.Form;
using Backend.Models;

namespace Backend.Repository.FormQuestion
{
    public class FormQuestionRepository : IFormQuestionRepository
    {
        private readonly DataContext _context;
        public FormQuestionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormQuestion(Models.Form.FormQuestion FormQuestion)
        {
            _context.Add(FormQuestion);
            return Save();
        }

        public bool DeleteFormQuestion(Models.Form.FormQuestion FormQuestion)
        {
            _context.Remove(FormQuestion);
            return Save();
        }

        public bool FormQuestionExists(int id)
        {
            return _context.FormQuestions.Any(m => m.Id == id);
        }


        public Models.Form.FormQuestion GetFormQuestion(int id)
        {
            return _context.FormQuestions.Where(m => m.Id == id).FirstOrDefault();
        }

        public int GetFormQuestionCount()
        {
            return _context.FormQuestions.Count();
        }

        public ICollection<Models.Form.FormQuestion> GetFormQuestions()
        {
            return _context.FormQuestions.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormQuestion(Models.Form.FormQuestion FormQuestion)
        {
            _context.Update(FormQuestion);
            return Save();
        }

        Models.Form.FormQuestion IFormQuestionRepository.GetFormQuestion(int id)
        {
            return _context.FormQuestions.Where(t => t.Id == id).FirstOrDefault();
        }
    }
}
