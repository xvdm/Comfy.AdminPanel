namespace AdminPanel.Services.Images.Remove;

public interface IRemoveImageFromFileSystemService
{
    public Task RemoveImage(string imageUrl);
}