using ClearReadHub.Documents.Domain.Storage.Entities;

namespace ClearReadHub.Documents.Application.Common.Interfaces;

public interface IStorageRepository
{
    Task AddAsync(StoredBlob blob, CancellationToken cancellationToken);
    Task<StoredBlob?> GetByIdAsync(Guid blobId, CancellationToken cancellationToken);
    Task RemoveAsync(StoredBlob blob, CancellationToken cancellationToken);
    Task UpdateAsync(StoredBlob blob, CancellationToken cancellationToken);
}