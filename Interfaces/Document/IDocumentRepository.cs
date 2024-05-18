using Backend.Models;

namespace Backend.Interfaces.Document
{
    public interface IDocumentRepository
    {
        ICollection<Models.Document.Document> GetDocuments();
        Models.Document.Document GetDocument(int id);
        Models.Document.Document GetDocument(string title);
        bool DocumentExists(int id);
        bool DocumentExists(string title);
        bool CreateDocument(Models.Document.Document document);
        bool UpdateDocument(Models.Document.Document document);
        bool DeleteDocument(Models.Document.Document document);
        bool Save();
    }
}
