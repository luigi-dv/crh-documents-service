using System.Text.Json.Serialization;

namespace ClearReadHub.Documents.Contracts.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
    Basic,
    Pro,
}