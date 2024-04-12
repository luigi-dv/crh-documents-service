using ClearReadHub.Documents.Application.Common.Interfaces;
using ClearReadHub.Documents.Application.Common.Security.Request;
using ClearReadHub.Documents.Infrastructure.Security.IdentityProvider;
using ClearReadHub.Documents.Infrastructure.Security.PolicyEnforcer;

using ErrorOr;

namespace ClearReadHub.Documents.Infrastructure.Security;

public class AuthorizationService(
    IPolicyEnforcer policyEnforcer,
    IIdentityProvider currentUserProvider)
    : IAuthorizationService
{
    public ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizeableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies)
    {
        var currentUser = currentUserProvider.GetCurrentUser();

        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return Error.Unauthorized(description: "User is missing required permissions for taking this action");
        }

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return Error.Unauthorized(description: "User is missing required roles for taking this action");
        }

        foreach (var policy in requiredPolicies)
        {
            var authorizationAgainstPolicyResult = policyEnforcer.Authorize(request, currentUser, policy);

            if (authorizationAgainstPolicyResult.IsError)
            {
                return authorizationAgainstPolicyResult.Errors;
            }
        }

        return Result.Success;
    }
}