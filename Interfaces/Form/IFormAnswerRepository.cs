using Backend.Models.Form;
using System.Collections.Generic;

namespace Backend.Interfaces.Form
{
    public interface IFormAnswerRepository
    {
        ICollection<FormAnswer> GetFormAnswers();
        FormAnswer GetFormAnswer(int id);
        ICollection<FormAnswer> GetFormAnswersByQuestionId(int questionId);
        IDictionary<int?, int> GetFormAnswerCountsByOptionId(int questionId);
        bool FormAnswerExists(int id);
        bool CreateFormAnswer(FormAnswer formAnswer);
        bool UpdateFormAnswer(FormAnswer formAnswer);
        bool DeleteFormAnswer(FormAnswer formAnswer);
        bool Save();
    }
}
