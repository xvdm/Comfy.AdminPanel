namespace AdminPanel.Services.Caching;

public interface IRemoveCacheService
{
    public Task RemoveAsync(string pattern);
}