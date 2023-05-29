namespace AdminPanel.Services.Images.Remove;

public interface IRemoveImageFromFileSystemService
{
    public Task RemoveAsync(string imageUrl);
    public Task RemoveRangeAsync(IEnumerable<string> imageUrl);
}