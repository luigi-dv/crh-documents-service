using Azure.Storage.Blobs.Models;

using Microsoft.AspNetCore.Http;

namespace ClearReadHub.Documents.Application.Common.Interfaces;

public interface IAzureStorageRepository
{
    Task AddAsync(IFormFile blob, CancellationToken cancellationToken);
    Task DeleteAsync(string name, CancellationToken cancellationToken);
    Task<BlobDownloadInfo> GetAsync(string name, CancellationToken cancellationToken);
    Task<Stream> DownloadAsync(string name, CancellationToken cancellationToken);
    Task UpdateAsync(IFormFile blob, CancellationToken cancellationToken);
}