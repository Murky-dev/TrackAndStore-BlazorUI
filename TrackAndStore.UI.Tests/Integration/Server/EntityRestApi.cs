using TrackAndStore.UI.Entity;

namespace TrackAndStore.UI.Tests.Integration.Server;

public class MockHttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name)
    {
        return new HttpClient();
    }
}

public class EntityRestApi
{
    [Fact]
    public async Task GetsTheCorrectMessage()
    {
        var httpClientFactory = new MockHttpClientFactory();
        var entityRepository = new EntityRepository(httpClientFactory);

        var message = await entityRepository.GetMessageAsync();
        Assert.NotNull(message);
        Assert.Equal("Testing one two three", message);
    }
}
