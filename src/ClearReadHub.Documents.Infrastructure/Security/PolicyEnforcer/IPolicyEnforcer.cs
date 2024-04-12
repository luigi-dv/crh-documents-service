using ClearReadHub.Documents.Application.Common.Security.Request;
using ClearReadHub.Documents.Infrastructure.Security.IdentityProvider;

using ErrorOr;

namespace ClearReadHub.Documents.Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizeableRequest<T> request,
        CurrentUser currentUser,
        string policy);
}