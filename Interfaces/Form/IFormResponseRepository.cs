using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormResponseRepository
    {
        ICollection<FormResponse> GetFormResponses();
        FormResponse GetFormResponse(int id);
        bool FormResponseExists(int id);
        bool CreateFormResponse(FormResponse formResponse);
        bool UpdateFormResponse(FormResponse formResponse);
        bool DeleteFormResponse(FormResponse formResponse);
        bool Save();
    }
}
