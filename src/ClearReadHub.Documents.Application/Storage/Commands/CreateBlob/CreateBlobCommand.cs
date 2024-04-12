using ClearReadHub.Documents.Application.Common.Security.Request;
using ClearReadHub.Documents.Domain.Storage.Entities;

using ErrorOr;

namespace ClearReadHub.Documents.Application.Storage.Commands.CreateBlob;

public record CreateBlobCommand(Guid UserId, string BlobName, string BlobContainerName) : IAuthorizeableRequest<ErrorOr<StoredBlob>>;