namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class StoredBlob
    {
        public const string BlobName = "BlobName";
        public const string BlobContainerName = "BlobContainerName";
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateTime CreatedAt = DateTime.UtcNow;
        public static readonly DateTime UpdatedAt = DateTime.UtcNow;
    }
}