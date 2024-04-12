using ClearReadHub.Documents.Application.Common.Interfaces;
using ClearReadHub.Documents.Domain.Storage.Entities;

using ErrorOr;

using MediatR;

namespace ClearReadHub.Documents.Application.Storage.Queries.GetBlob;

public class GetBlobQueryHandler(IStorageRepository storageRepository)
    : IRequestHandler<GetBlobQuery, ErrorOr<StoredBlob>>
{
    public async Task<ErrorOr<StoredBlob>> Handle(GetBlobQuery query, CancellationToken cancellationToken)
    {
        var storedBlob = await storageRepository.GetByIdAsync(query.BlobId, cancellationToken);
        if (storedBlob == null)
        {
            return Error.NotFound("Blob not found");
        }

        return storedBlob;
    }
}