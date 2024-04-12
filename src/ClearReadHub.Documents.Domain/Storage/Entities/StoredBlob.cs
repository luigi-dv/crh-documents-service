using ClearReadHub.Documents.Domain.Common;

namespace ClearReadHub.Documents.Domain.Storage.Entities;

public class StoredBlob : Entity
{
    public string BlobName { get; set; } = string.Empty;
    public string BlobContainerName { get; set; } = string.Empty;
}