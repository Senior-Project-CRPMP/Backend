using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;

namespace Backend.Repository
{
    public class FormLinkQuestionRepository : IFormLinkQuestionRepository
    {
        private readonly DataContext _context;
        public FormLinkQuestionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormLinkQuestion(FormLinkQuestion FormLinkQuestion)
        {
            _context.Add(FormLinkQuestion);
            return Save();
        }

        public bool DeleteFormLinkQuestion(FormLinkQuestion FormLinkQuestion)
        {
            _context.Remove(FormLinkQuestion);
            return Save();
        }

        public FormLinkQuestion GetFormLinkQuestion(int id)
        {
            return _context.FormLinkQuestions.Where(f => f.Id == id).FirstOrDefault();
        }

        public ICollection<FormLinkQuestion> GetFormLinkQuestions()
        {
            return _context.FormLinkQuestions.OrderBy(f => f.Id).ToList();
        }

        public bool FormLinkQuestionExists(int id)
        {
            return _context.FormLinkQuestions.Any(f => f.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormLinkQuestion(FormLinkQuestion FormLinkQuestion)
        {
            _context.Update(FormLinkQuestion);
            return Save();
        }
    }
}
