using ClearReadHub.Documents.Domain.Documents.Entities;

namespace ClearReadHub.Documents.Domain.Documents.Repositories;

public interface IDocumentRepository
{
    Task<Document> GetByIdAsync(Guid id);
    Task SaveAsync(Document document);
}