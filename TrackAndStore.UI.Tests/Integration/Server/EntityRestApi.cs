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

[Collection("Sequential")]
public class EntityRestApi : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        // Rebuild database for each test.
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:3000/dev/v1/migrations/");
        await httpClient.GetAsync("reset");
        await httpClient.GetAsync("testdata");
    }

    public async Task DisposeAsync() { }

    private EntityRepository EntityRepository()
    {
        var httpClientFactory = new MockHttpClientFactory();
        return new EntityRepository(httpClientFactory);
    }

    [Fact]
    public async Task GetsTheCorrectEntityById()
    {
        var entity = await EntityRepository().GetByIdAsync(1);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal("Living room", entity.Name);
    }

    [Fact]
    public async Task GetsAllTheEntities()
    {
        var entities = await EntityRepository().GetAsync();
        Assert.Equal(7, entities.Count);
        Assert.Equal("Pocket Watch", entities[6].Name);
    }

    [Fact]
    public async Task CreatesAValidEntityWithAllFields()
    {
        EntityCreateDto validEntity = new()
        {
            ParentId = null,
            EntityType = EntityType.Location,
            Name = "Garage",
            Description = "The place where a car should be but never is."
        };

        var entity = await EntityRepository().CreateAsync(validEntity);
        Assert.NotNull(entity);
        Assert.Equal(validEntity.ParentId, entity.ParentId);
        Assert.Equal(validEntity.EntityType, entity.EntityType);
        Assert.Equal(validEntity.Name, entity.Name);
        Assert.Equal(validEntity.Description, entity.Description);
    }

    [Fact]
    public async Task UpdatesAValidEntity()
    {
        Entity.Entity validEntity = new()
        {
            Id = 4,
            ParentId = 2,
            Name = "Glasses wipes",
            EntityType = EntityType.Item,
            Description = "Box of glasses wipes"
        };

        await EntityRepository().UpdateAsync(validEntity);
        var updatedEntity = await EntityRepository().GetByIdAsync(validEntity.Id);

        Assert.NotNull(updatedEntity);
        Assert.Equal(validEntity.Id, updatedEntity.Id);
        Assert.Equal(validEntity.ParentId, updatedEntity.ParentId);
        Assert.Equal(validEntity.Name, updatedEntity.Name);
        Assert.Equal(validEntity.EntityType, updatedEntity.EntityType);
        Assert.Equal(validEntity.Description, updatedEntity.Description);
    }

    [Fact]
    public async Task DeletesAnExistingEntity()
    {
        int entityId = 7;

        await EntityRepository().DeleteAsync(entityId);
        var deletedEntity = await EntityRepository().GetByIdAsync(entityId);

        Assert.Null(deletedEntity);
    }
}
