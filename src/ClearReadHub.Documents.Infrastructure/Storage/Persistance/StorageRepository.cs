using ClearReadHub.Documents.Application.Common.Interfaces;
using ClearReadHub.Documents.Domain.Storage.Entities;
using ClearReadHub.Documents.Infrastructure.Common.Persistance;

using Microsoft.EntityFrameworkCore;

namespace ClearReadHub.Documents.Infrastructure.Storage.Persistance;

public class StorageRepository(DocumentsDbContext dbContext) : IStorageRepository
{
    public async Task AddAsync(StoredBlob storedBlob, CancellationToken cancellationToken)
    {
       await dbContext.AddAsync(storedBlob, cancellationToken);
       await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<StoredBlob?> GetByIdAsync(Guid blobId, CancellationToken cancellationToken)
    {
        return await dbContext.StoredBlobs.FirstOrDefaultAsync(x => x.Id == blobId, cancellationToken);
    }

    public async Task RemoveAsync(StoredBlob blob, CancellationToken cancellationToken)
    {
        dbContext.Remove(blob);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(StoredBlob blob, CancellationToken cancellationToken)
    {
        dbContext.Update(blob);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}