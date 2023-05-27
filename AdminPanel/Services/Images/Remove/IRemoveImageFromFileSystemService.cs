namespace AdminPanel.Services.Images.Remove;

public interface IRemoveImageFromFileSystemService
{
    public Task Remove(string imageUrl);
    public Task RemoveRange(IEnumerable<string> imageUrl);
}