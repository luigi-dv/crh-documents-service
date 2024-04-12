using ClearReadHub.Documents.Contracts.Common;

namespace ClearReadHub.Documents.Contracts.Tokens;

public record TokenResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    SubscriptionType SubscriptionType,
    string Token);