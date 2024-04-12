using ClearReadHub.Documents.Contracts.Common;

namespace ClearReadHub.Documents.Application.Tokens.Queries.Generate;

public record GenerateTokenResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    SubscriptionType SubscriptionType,
    string Token);