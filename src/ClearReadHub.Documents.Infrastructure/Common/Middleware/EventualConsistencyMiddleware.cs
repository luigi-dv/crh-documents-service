using ClearReadHub.Documents.Domain.Common;
using ClearReadHub.Documents.Infrastructure.Common.Persistance;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace ClearReadHub.Documents.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    public const string DomainEventsKey = "DomainEventsKey";

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, DocumentsDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                {
                    while (domainEvents.TryDequeue(out var nextEvent))
                    {
                        await publisher.Publish(nextEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await next(context);
    }
}