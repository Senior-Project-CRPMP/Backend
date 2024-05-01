namespace Backend.Interfaces.Form
{
    public interface IFormQuestionRepository
    {
        ICollection<Models.Form.FormQuestion> GetFormQuestions();
        int GetFormQuestionCount();
        Models.Form.FormQuestion GetFormQuestion(int id);
        bool FormQuestionExists(int id);
        bool CreateFormQuestion(Models.Form.FormQuestion formQuestion);
        bool UpdateFormQuestion(Models.Form.FormQuestion formQuestion);
        bool DeleteFormQuestion(Models.Form.FormQuestion formQuestion);
        bool Save();
    }
}
