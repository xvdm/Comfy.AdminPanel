namespace AdminPanel.Interfaces;

public interface ICacheable
{
    public string CacheKey { get; }
    public double ExpirationHours { get; }
}