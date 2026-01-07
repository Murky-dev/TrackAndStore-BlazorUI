namespace TrackAndStore.UI.Entity;

public class EntityRepository(IHttpClientFactory httpClientFactory)
{
    public async Task<Entity?> GetEntityByIdAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient();
        // TODO: Set the base URL somewhere common (can we set this in the factory?)
        return await httpClient.GetFromJsonAsync<Entity>($"http://localhost:3000/api/v1/entities/{id}");
    }

    public async Task<List<Entity>> GetEntitiesAsync()
    {
        // TODO: Pull  out into a property and/or base class.
        var httpClient = httpClientFactory.CreateClient();
        var entities = await httpClient.GetFromJsonAsync<List<Entity>>($"http://localhost:3000/api/v1/entities/");

        if (entities == null)
            return new List<Entity>();
        else
            return entities;
    }
}
