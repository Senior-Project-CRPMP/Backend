using Backend.Models;
using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormLinkQuestionRepository
    {
        ICollection<FormLinkQuestion> GetFormLinkQuestions();
        FormLinkQuestion GetFormLinkQuestion(int id);
        bool FormLinkQuestionExists(int id);
        bool CreateFormLinkQuestion(FormLinkQuestion formLinkQuestion);
        bool UpdateFormLinkQuestion(FormLinkQuestion formLinkQuestion);
        bool DeleteFormLinkQuestion(FormLinkQuestion formLinkQuestion);
        bool Save();
    }
}
