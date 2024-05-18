using Backend.Models.Form;

namespace Backend.Interfaces.Form
{
    public interface IFormFileStorageRepository
    {
        ICollection<FormFileStorage> GetFormFileStorages();
        FormFileStorage GetFormFileStorage(int id);
        bool FormFileStorageExists(int id);
        bool CreateFormFileStorage(FormFileStorage formFileStorage);
        bool UpdateFormFileStorage(FormFileStorage formFileStorage);
        bool DeleteFormFileStorage(FormFileStorage formFileStorage);
        bool Save();
    }
}
