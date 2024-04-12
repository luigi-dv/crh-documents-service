namespace ClearReadHub.Documents.Infrastructure.Security.IdentityProvider;

public interface IIdentityProvider
{
    CurrentUser GetCurrentUser();
}