using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormOptionRepository
    {
        ICollection<FormOption> GetFormOptions();
        FormOption GetFormOption(int id);
        bool FormOptionExists(int id);
        bool CreateFormOption(FormOption formOption);
        bool UpdateFormOption(FormOption formOption);
        bool DeleteFormOption(FormOption formOption);
        bool Save();
    }
}
