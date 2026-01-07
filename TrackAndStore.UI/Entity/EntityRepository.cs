namespace TrackAndStore.UI.Entity;

public class EntityRepository(IHttpClientFactory httpClientFactory)
{
    public async Task<Entity?> GetEntityByIdAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient("APIv1");
        // TODO: Set the base URL somewhere common (can we set this in the factory?)
        return await httpClient.GetFromJsonAsync<Entity>($"entities/{id}");
    }

    public async Task<List<Entity>> GetEntitiesAsync()
    {
        // TODO: Pull  out into a property and/or base class.
        var httpClient = httpClientFactory.CreateClient("APIv1");
        var entities = await httpClient.GetFromJsonAsync<List<Entity>>($"entities/");

        if (entities == null)
            return new List<Entity>();
        else
            return entities;
    }
}
