using ClearReadHub.Documents.Domain.Common;

namespace ClearReadHub.Documents.Domain.Documents.Entities;

public class Document : Entity
{
    public string? Name { get; set; }
}