using ClearReadHub.Documents.Domain.Storage.Entities;

using ErrorOr;

using MediatR;

namespace ClearReadHub.Documents.Application.Storage.Queries.GetBlob;

public record GetBlobQuery(Guid BlobId) : IRequest<ErrorOr<StoredBlob>>;
