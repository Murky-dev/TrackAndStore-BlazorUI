namespace TrackAndStore.UI.Entity;

// TODO: Error handling (when properly supported by the REST API).
public class EntityRepository(IHttpClientFactory httpClientFactory)
{
    private HttpClient HttpClient()
    {
        return httpClientFactory.CreateClient("APIv1");
    }

    public async Task<Entity?> GetByIdAsync(int id)
    {
        try
        {
            return await HttpClient().GetFromJsonAsync<Entity>($"entities/{id}");
        }
        catch (HttpRequestException e)
        {
            return null;
        }
    }

    public async Task<List<Entity>> GetAsync()
    {
        var entities = await HttpClient().GetFromJsonAsync<List<Entity>>($"entities/");

        if (entities == null)
            return new List<Entity>();
        else
            return entities;
    }

    public async Task<Entity?> CreateAsync(EntityCreateDto dto)
    {
        var response = await HttpClient().PostAsJsonAsync<EntityCreateDto>($"entities/", dto);
        return await response.Content.ReadFromJsonAsync<Entity>();
    }

    public async Task UpdateAsync(Entity entity)
    {
        await HttpClient().PatchAsJsonAsync<Entity>($"entities/{entity.Id}", entity);
    }

    public async Task DeleteAsync(int id)
    {
        await HttpClient().DeleteAsync($"entities/{id}");
    }

    public async Task DeleteAsync(Entity entity)
    {
        await DeleteAsync(entity.Id);
    }
}
