namespace AdminPanel.Services.Images.Remove;

public sealed class RemoveImageFromWwwRootService : IRemoveImageFromFileSystemService
{
    private readonly IWebHostEnvironment _env;

    public RemoveImageFromWwwRootService(IWebHostEnvironment env)
    {
        _env = env;
    }


    public void RemoveImage(string imageUrl)
    {
        var fullPath = Path.Combine(_env.WebRootPath, imageUrl);
        if (fullPath == imageUrl) fullPath = _env.WebRootPath + imageUrl;

        if (!File.Exists(fullPath)) return;

        try
        {
            File.Delete(fullPath);
        }
        catch (Exception e)
        {

        }
    }
}