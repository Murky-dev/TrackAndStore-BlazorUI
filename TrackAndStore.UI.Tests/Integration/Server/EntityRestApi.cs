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
    private EntityRepository EntityRepository()
    {
        var httpClientFactory = new MockHttpClientFactory();
        return new EntityRepository(httpClientFactory);
    }

    [Fact(Skip = "The REST API has a bug: it's returning the object wrapped in an array.")]
    public async Task GetsTheCorrectEntityById()
    {
        var entity = await EntityRepository().GetByIdAsync(1);
        Assert.NotNull(entity);
        Assert.Equal(1, entity.Id);
        Assert.Equal("Home", entity.Name);
    }

    [Fact]
    public async Task GetsAllTheEntities()
    {
        var entities = await EntityRepository().GetAsync();
        Assert.Equal(15, entities.Count);
        Assert.Equal("Hat", entities[14].Name);
    }

    [Fact(Skip = "The REST API has a bug: it's returning the object wrapped in an array.")]
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

    [Fact(Skip = "The REST API has a bug: it's returning the object wrapped in an array.")]
    public async Task UpdatesAValidEntity()
    {
        Entity.Entity validEntity = new()
        {
            Id = 8,
            ParentId = 5,
            Name = "Wallet",
            EntityType = EntityType.Container,
            Description = "A relic of the past?"
        };

        await EntityRepository().UpdateAsync(validEntity);
        var updatedEntity = await EntityRepository().GetByIdAsync(validEntity.Id);

        Assert.NotNull(updatedEntity);
        Assert.Equal(validEntity, updatedEntity);
    }

    [Fact(Skip = "The REST API has a bug: it's returning the object wrapped in an array.")]
    public async Task DeletesAnExistingEntity()
    {
        int entityId = 7;

        await EntityRepository().DeleteAsync(entityId);
        var deletedEntity = await EntityRepository().GetByIdAsync(entityId);

        Assert.Null(deletedEntity);
    }
}
