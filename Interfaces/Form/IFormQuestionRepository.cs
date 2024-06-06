using System.Collections.Generic;
using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormQuestionRepository
    {
        ICollection<FormQuestion> GetFormQuestions();
        int GetFormQuestionCount();
        FormQuestion GetFormQuestion(int id);
        bool FormQuestionExists(int id);
        bool CreateFormQuestion(FormQuestion formQuestion);
        bool UpdateFormQuestion(FormQuestion formQuestion);
        bool DeleteFormQuestion(FormQuestion formQuestion);
        bool Save();
        ICollection<FormQuestion> GetQuestionsByFormId(int formId);
    }
}
