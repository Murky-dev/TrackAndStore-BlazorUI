using TrackAndStore.UI.Entity;

namespace TrackAndStore.UI.Tests.Integration.Server;

public class MockHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:3000/api/v1/");

        return httpClient;
    }
}

public class EntityRestApi
{
    [Fact(Skip = "The REST API has a bug: it's returning the object wrapped in an array.")]
    public async Task GetsTheCorrectEntityById()
    {
        // TODO: Pull this out into properties.
        var httpClientFactory = new MockHttpClientFactory();
        var entityRepository = new EntityRepository(httpClientFactory);

        var entity = await entityRepository.GetEntityByIdAsync(1);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal("Home", entity.Name);
    }

    [Fact]
    public async Task GetsAllTheEntities()
    {
        // TODO: Pull this out into properties.
        var httpClientFactory = new MockHttpClientFactory();
        var entityRepository = new EntityRepository(httpClientFactory);

        var entities = await entityRepository.GetEntitiesAsync();
        Assert.Equal(15, entities.Count);
        Assert.Equal("Hat", entities[14].Name);
    }
}
