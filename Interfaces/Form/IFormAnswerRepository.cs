using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormAnswerRepository
    {
        ICollection<FormAnswer> GetFormAnswers();
        FormAnswer GetFormAnswer(int id);
        bool FormAnswerExists(int id);
        bool CreateFormAnswer(FormAnswer formAnswer);
        bool UpdateFormAnswer(FormAnswer formAnswer);
        bool DeleteFormAnswer(FormAnswer formAnswer);
        bool Save();
    }
}
