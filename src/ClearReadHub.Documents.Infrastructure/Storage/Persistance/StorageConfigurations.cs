using ClearReadHub.Documents.Domain.Storage.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearReadHub.Documents.Infrastructure.Storage.Persistance;

public class StorageConfigurations : IEntityTypeConfiguration<StoredBlob>
{
    public void Configure(EntityTypeBuilder<StoredBlob> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(r => r.BlobName);
        builder.Property(r => r.BlobContainerName);
        builder.Property(r => r.CreatedAt);
        builder.Property(r => r.UpdatedAt);
    }
}