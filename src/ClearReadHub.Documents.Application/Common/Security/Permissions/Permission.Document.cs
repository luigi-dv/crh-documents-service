namespace ClearReadHub.Documents.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Document
    {
        public const string Create = "create:document";
        public const string Read = "read:document";
        public const string Update = "update:document";
        public const string Delete = "delete:document";
    }
}