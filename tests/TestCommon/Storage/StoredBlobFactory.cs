using ClearReadHub.Documents.Domain.Storage.Entities;

using TestCommon.TestConstants;

namespace TestCommon.Storage;

public static class StorageBlobFactory
{
    public static StoredBlob CreateStoredBlob(
        Guid? id = null,
        string blobName = Constants.StoredBlob.BlobName,
        string blobContainerName = Constants.StoredBlob.BlobContainerName)
    {
        return new StoredBlob() { BlobContainerName = blobContainerName, BlobName = blobName, };
    }
}