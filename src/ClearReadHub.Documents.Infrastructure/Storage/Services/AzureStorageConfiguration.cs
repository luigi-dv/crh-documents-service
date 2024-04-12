namespace ClearReadHub.Documents.Infrastructure.Storage.Services;

public class AzureStorageConfiguration
{
    public string? AccountName { get; set; }
    public string? AccountKey { get; set; }
    public string? ContainerName { get; set; }
    public bool UseDevelopmentStorage { get; set; } = false;
}