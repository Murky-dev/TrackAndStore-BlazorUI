namespace TrackAndStore.UI.Entity;

public class EntityRepository(IHttpClientFactory httpClientFactory)
{
    private HttpClient HttpClient()
    {
        return httpClientFactory.CreateClient("APIv1");
    }

    public async Task<Entity?> GetEntityByIdAsync(int id)
    {
        return await HttpClient().GetFromJsonAsync<Entity>($"entities/{id}");
    }

    public async Task<List<Entity>> GetEntitiesAsync()
    {
        var entities = await HttpClient().GetFromJsonAsync<List<Entity>>($"entities/");

        if (entities == null)
            return new List<Entity>();
        else
            return entities;
    }
}
