using Backend.Data;
using Backend.Interfaces.Form;
using Backend.Models.Form;
using System.Collections.Generic;
using System.Linq;

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

        public ICollection<FormAnswer> GetFormAnswersByQuestionId(int questionId)
        {
            return _context.FormAnswers.Where(fa => fa.FormQuestionId == questionId && fa.FormOptionId == null).ToList();
        }

        public IDictionary<int?, int> GetFormAnswerCountsByOptionId(int questionId, int? optionId = null)
        {
            var query = _context.FormAnswers.Where(fa => fa.FormQuestionId == questionId);

            if (optionId.HasValue)
            {
                query = query.Where(fa => fa.FormOptionId == optionId.Value);
            }

            return query.GroupBy(fa => fa.FormOptionId)
                        .ToDictionary(g => g.Key, g => g.Count());
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

        public int GetFormAnswerCountByOptionId(int optionId)
        {
            return _context.FormAnswers.Count(fa => fa.FormOptionId == optionId);
        }

    }
}
