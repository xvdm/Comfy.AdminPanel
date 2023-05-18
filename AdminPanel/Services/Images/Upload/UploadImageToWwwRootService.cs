namespace AdminPanel.Services.Images.Upload;

public sealed class UploadImageToWwwRootService : IUploadImageToFileSystemService
{
    private readonly IWebHostEnvironment _env;

    public UploadImageToWwwRootService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadImageAsync(IFormFile imageFile)
    {
        var fileType = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf('.') + 1);
        var guid = Guid.NewGuid();
        var path = $"/images/{guid}.{fileType}";

        await using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }
        return path;
    }
}