namespace TrackAndStore.UI.Entity;

public class EntityRepository(IHttpClientFactory httpClientFactory)
{
    public async Task<string> GetMessageAsync()
    {
        var httpClient = httpClientFactory.CreateClient();
        return await httpClient.GetStringAsync("https://www.google.com/");
    }
}
