namespace AdminPanel.Interfaces;

public interface ICacheInvalidation
{
    public string[] CacheKeys { get; }
}