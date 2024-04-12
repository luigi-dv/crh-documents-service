using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using ClearReadHub.Documents.Application.Common.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ClearReadHub.Documents.Infrastructure.Storage.Services;

public class AzureStorageRepository : IAzureStorageRepository
{
    private readonly BlobContainerClient _fileContainer;

    public AzureStorageRepository(IOptions<AzureStorageConfiguration> configuration)
    {
        var config = configuration.Value;
        var credential = new StorageSharedKeyCredential(config.AccountName, config.AccountKey);
        var blobUri = config.UseDevelopmentStorage ?
            new Uri($"http://localhost:10000/{config.AccountName}") :
            new Uri($"https://{config.AccountName}.blob.core.windows.net");
        var blobServiceClient = new BlobServiceClient(blobUri, credential);
        _fileContainer = blobServiceClient.GetBlobContainerClient(config.ContainerName); // Use configuration for container name
    }

    public async Task AddAsync(IFormFile blob, CancellationToken cancellationToken)
    {
        BlobClient client = _fileContainer.GetBlobClient(blob.FileName);
        await using (Stream? data = blob.OpenReadStream())
        {
            await client.UploadAsync(data, true,  cancellationToken: cancellationToken);
        }
    }

    public async Task<BlobDownloadInfo> GetAsync(string blobName, CancellationToken cancellationToken)
    {
        BlobClient client = _fileContainer.GetBlobClient(blobName);

        if (!await client.ExistsAsync(cancellationToken))
        {
            throw new Exception("Blob not found");
        }

        var content = await client.DownloadAsync(cancellationToken: cancellationToken);

        return content;
    }

    public async Task UpdateAsync(IFormFile blob, CancellationToken cancellationToken)
    {
        BlobClient client = _fileContainer.GetBlobClient(blob.FileName);
        if (!await client.ExistsAsync(cancellationToken))
        {
            throw new Exception("Blob not found");
        }

        await using (Stream? data = blob.OpenReadStream())
        {
            await client.UploadAsync(data, true, cancellationToken: cancellationToken);
        }
    }

    public async Task<Stream> DownloadAsync(string blobName, CancellationToken cancellationToken)
    {
        BlobClient client = _fileContainer.GetBlobClient(blobName);
        if (!await client.ExistsAsync(cancellationToken))
        {
            throw new Exception("Blob not found");
        }

        var content = await client.OpenReadAsync(cancellationToken: cancellationToken);

        return content;
    }

    public async Task DeleteAsync(string blobName, CancellationToken cancellationToken)
    {
        BlobClient client = _fileContainer.GetBlobClient(blobName);

        if (!await client.ExistsAsync(cancellationToken))
        {
            throw new Exception("Blob not found");
        }

        await client.DeleteAsync(cancellationToken: cancellationToken);
    }
}