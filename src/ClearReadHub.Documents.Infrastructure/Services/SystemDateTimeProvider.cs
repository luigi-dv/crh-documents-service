using ClearReadHub.Documents.Application.Common.Interfaces;

namespace ClearReadHub.Documents.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}