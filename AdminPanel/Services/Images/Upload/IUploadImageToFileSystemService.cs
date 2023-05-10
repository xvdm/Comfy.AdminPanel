namespace AdminPanel.Services.Images.Upload;

public interface IUploadImageToFileSystemService
{
    public Task<string> UploadImage(IFormFile imageFile);
}