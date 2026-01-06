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
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = default!;
    public string EntityType { get; set; } = default!;
    public string Description { get; set; } = default!;
}
