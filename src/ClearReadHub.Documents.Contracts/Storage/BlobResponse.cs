namespace ClearReadHub.Documents.Contracts.Storage;

public record BlobResponse(
    Guid Id,
    string BlobName,
    string BlobContainerName,
    DateTime CreatedAt,
    DateTime UpdatedAt);