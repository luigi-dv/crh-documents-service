using ClearReadHub.Documents.Application.Common.Interfaces;
using ClearReadHub.Documents.Domain.Storage.Entities;

using ErrorOr;

using MediatR;

namespace ClearReadHub.Documents.Application.Storage.Commands.CreateBlob;

public class CreateBlobCommandHandler(
    IStorageRepository repository) : IRequestHandler<CreateBlobCommand, ErrorOr<StoredBlob>>
{
    public async Task<ErrorOr<StoredBlob>> Handle(CreateBlobCommand command, CancellationToken cancellationToken)
    {
        var storedBlob = new StoredBlob
        {
            BlobName = command.BlobName,
            BlobContainerName = command.BlobContainerName,
        };

        await repository.AddAsync(storedBlob, cancellationToken);

        return storedBlob;
    }
}