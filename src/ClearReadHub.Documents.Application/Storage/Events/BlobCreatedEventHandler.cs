using ClearReadHub.Documents.Application.Common.Interfaces;

using MediatR;

namespace ClearReadHub.Documents.Application.Storage.Events;

public class BlobCreatedEventHandler(IAzureStorageRepository azureStorageRepository) : INotificationHandler<BlobCreatedEvent>
{
    public async Task Handle(BlobCreatedEvent notification, CancellationToken cancellationToken)
    {
        await azureStorageRepository.AddAsync(notification.Blob, cancellationToken);
    }
}