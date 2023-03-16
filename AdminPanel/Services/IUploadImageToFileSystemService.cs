namespace AdminPanel.Services
{
    public interface IUploadImageToFileSystemService
    {
        public Task<string> UploadImage(IFormFile imageFile);
    }
}
