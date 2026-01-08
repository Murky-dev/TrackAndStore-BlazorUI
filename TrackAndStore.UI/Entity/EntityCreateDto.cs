using System.Text.Json.Serialization;

namespace TrackAndStore.UI.Entity;

public record EntityCreateDto
{
    [JsonPropertyName("parent_id")]
    public int? ParentId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; } = default!;

    [JsonPropertyName("entity_type")]
    public required string EntityType { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = default!;
}
