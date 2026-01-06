namespace TrackAndStore.UI.Entity;

public class EntityRepository
{
    private HttpClient httpClient;

    public EntityRepository()
    {
        httpClient = new HttpClient();
    }

    public string GetMessage()
    {
        return "One.. two.. three four five..";
    }
}
