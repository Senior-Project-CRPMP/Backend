using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;

namespace Backend.Repository.Form
{
    public class FormAnswerRepository : IFormAnswerRepository
    {
        private readonly DataContext _context;
        public FormAnswerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFormAnswer(FormAnswer formAnswer)
        {
            _context.Add(formAnswer);
            return Save();
        }

        public bool DeleteFormAnswer(FormAnswer formAnswer)
        {
            _context.Remove(formAnswer);
            return Save();
        }

        public FormAnswer GetFormAnswer(int id)
        {
            return _context.FormAnswers.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<FormAnswer> GetFormAnswers()
        {
            return _context.FormAnswers.OrderBy(p => p.Id).ToList();
        }

        public bool FormAnswerExists(int id)
        {
            return _context.FormAnswers.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFormAnswer(FormAnswer formAnswer)
        {
            _context.Update(formAnswer);
            return Save();
        }
    }
}
