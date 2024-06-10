using Backend.Data;
using Backend.Interfaces.Document;
using Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository.Document
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDocument(Models.Document.Document document)
        {
            _context.Add(document);
            return Save();
        }

        public bool DeleteDocument(Models.Document.Document document)
        {
            _context.Remove(document);
            return Save();
        }

        public Models.Document.Document GetDocument(string title)
        {
            return _context.Documents.Where(p => p.Title == title).FirstOrDefault();
        }

        public Models.Document.Document GetDocument(int id)
        {
            return _context.Documents.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Models.Document.Document> GetDocuments()
        {
            return _context.Documents.OrderBy(p => p.Id).ToList();
        }

        public bool DocumentExists(int id)
        {
            return _context.Documents.Any(p => p.Id == id);
        }

        public bool DocumentExists(string title)
        {
            return _context.Documents.Any(p => p.Title == title);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateDocument(Models.Document.Document document)
        {
            _context.Update(document);
            return Save();
        }

        public ICollection<Models.Document.Document> GetDocumentsByProjectId(int projectId)
        {
            return _context.Documents.Where(d => d.ProjectId == projectId).ToList();
        }
    }
}
