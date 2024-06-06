using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;

namespace Backend.Repository.FormQuestion
{
    public class FormQuestionRepository : IFormQuestionRepository
    {
        private readonly DataContext _context;
        public FormQuestionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormQuestion(Models.Form.FormQuestion formQuestion)
        {
            _context.Add(formQuestion);
            return Save();
        }

        public bool DeleteFormQuestion(Models.Form.FormQuestion formQuestion)
        {
            _context.Remove(formQuestion);
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

        public bool UpdateFormQuestion(Models.Form.FormQuestion formQuestion)
        {
            _context.Update(formQuestion);
            return Save();
        }

        public ICollection<Models.Form.FormQuestion> GetQuestionsByFormId(int formId)
        {
            return _context.FormQuestions.Where(q => q.FormId == formId).ToList();
        }


    }
}
