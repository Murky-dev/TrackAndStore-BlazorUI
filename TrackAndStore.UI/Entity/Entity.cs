using System.Text.Json.Serialization;

namespace TrackAndStore.UI.Entity;

public static class EntityType
{
    public static readonly string Item = "item";
    public static readonly string Container = "container";
    public static readonly string Person = "person";
    public static readonly string Location = "location";
}

public class Entity
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("parent_id")]
    public int? ParentId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; } = default!;

    [JsonPropertyName("entity_type")]
    public required string EntityType { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = default!;
}
