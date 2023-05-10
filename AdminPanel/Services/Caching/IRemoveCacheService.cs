namespace AdminPanel.Services.Caching;

public interface IRemoveCacheService
{
    public Task Remove(string pattern);
}