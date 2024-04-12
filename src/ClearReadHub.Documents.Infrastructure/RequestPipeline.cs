using ClearReadHub.Documents.Infrastructure.Common.Middleware;

using Microsoft.AspNetCore.Builder;

namespace ClearReadHub.Documents.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}