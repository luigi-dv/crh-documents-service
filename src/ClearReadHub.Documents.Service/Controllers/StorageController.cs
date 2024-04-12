using System.ComponentModel.DataAnnotations;

using ClearReadHub.Documents.Application.Storage.Commands.CreateBlob;
using ClearReadHub.Documents.Application.Storage.Events;
using ClearReadHub.Documents.Application.Storage.Queries.GetBlob;
using ClearReadHub.Documents.Contracts.Storage;
using ClearReadHub.Documents.Domain.Storage.Entities;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClearReadHub.Documents.Service.Controllers;

[Route("storage")]
public class StorageController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> UploadBlob([FromForm] CreateBlobRequest request)
    {
        var containerName = "clearreadhub";
        var userId = new Guid("00000000-0000-0000-0000-000000000000");
        var command = new CreateBlobCommand(userId, request.Blob.FileName, containerName);

        var result = await mediator.Send(command);

        if (result.IsError)
        {
            return Problem(result.Errors.First().Description);
        }

        var storedBlob = result.Value;

        // Trigger the BlobCreatedEvent event
        await mediator.Publish(new BlobCreatedEvent(request.Blob, storedBlob));

        return CreatedAtAction(
            actionName: nameof(GetBlob),
            routeValues: new { storedBlob.Id },
            value: ToDto(storedBlob));
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetBlob([FromRoute, Required]Guid Id)
    {
        var command = new GetBlobQuery(Id);
        var result = await mediator.Send(command);

        return result.Match(
            blob => Ok(ToDto(blob)),
            Problem);
    }

    private BlobResponse ToDto(StoredBlob storedBlob) =>
        new(storedBlob.Id, storedBlob.BlobName, storedBlob.BlobContainerName, storedBlob.CreatedAt, storedBlob.UpdatedAt);
}
