using Backend.Models.Form;
using System.Collections.Generic;

namespace Backend.Interfaces.Form
{
    public interface IFormResponseRepository
    {
        ICollection<FormResponse> GetFormResponses();
        FormResponse GetFormResponse(int id);
        int GetFormResponseCount();
        int GetFormResponseCountByFormId(int formId);
        bool FormResponseExists(int id);
        bool CreateFormResponse(FormResponse formResponse);
        bool UpdateFormResponse(FormResponse formResponse);
        bool DeleteFormResponse(FormResponse formResponse);
        bool Save();
    }
}
