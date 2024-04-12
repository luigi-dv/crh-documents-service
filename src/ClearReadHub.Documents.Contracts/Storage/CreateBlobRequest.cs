using Microsoft.AspNetCore.Http;

namespace ClearReadHub.Documents.Contracts.Storage;

public record CreateBlobRequest(IFormFile Blob);