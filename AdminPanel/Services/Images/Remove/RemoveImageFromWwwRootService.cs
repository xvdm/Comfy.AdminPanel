namespace AdminPanel.Services.Images.Remove;

public sealed class RemoveImageFromWwwRootService : IRemoveImageFromFileSystemService
{
    private readonly IWebHostEnvironment _env;

    public RemoveImageFromWwwRootService(IWebHostEnvironment env)
    {
        _env = env;
    }


    public async Task RemoveImage(string imageUrl)
    {
        await Task.CompletedTask;

        var fullPath = Path.Combine(_env.WebRootPath, imageUrl);
        if (fullPath == imageUrl) fullPath = _env.WebRootPath + imageUrl;

        if (!File.Exists(fullPath)) return;

        try
        {
            File.Delete(fullPath);
        }
        catch
        {
            // ignored
        }
    }
}