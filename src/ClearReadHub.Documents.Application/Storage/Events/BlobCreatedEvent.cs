using ClearReadHub.Documents.Domain.Storage.Entities;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ClearReadHub.Documents.Application.Storage.Events;

public record BlobCreatedEvent(IFormFile Blob, StoredBlob StoredBlob) : INotification;
