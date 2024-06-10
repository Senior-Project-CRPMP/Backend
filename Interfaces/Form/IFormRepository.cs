using Backend.Models;
using System.Collections.Generic;

namespace Backend.Interfaces.Form
{
    public interface IFormRepository
    {
        ICollection<Models.Form.Form> GetForms();
        ICollection<Models.Form.Form> GetProjectForms(int id);
        int GetFormCount(); 
        Models.Form.Form GetForm(int id);
        Models.Form.Form GetForm(string title);
        bool FormExists(int id);
        bool FormExists(string title);
        bool ProjectFormExists(int id);
        bool CreateForm(Models.Form.Form form);
        bool UpdateForm(Models.Form.Form form);
        bool DeleteForm(Models.Form.Form form);
        bool Save();
        ICollection<Models.Form.Form> GetFormsWithResponsesByProjectId(int projectId);
        ICollection<Models.Form.Form> GetFormsByProjectIdWithResponses(int projectId);
    }
}
