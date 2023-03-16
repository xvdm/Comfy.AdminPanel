using MediatR;

namespace AdminPanel.Services
{
    public class UploadImageToWwwRootService : IUploadImageToFileSystemService
    {
        private readonly IWebHostEnvironment _env;

        public UploadImageToWwwRootService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadImage(IFormFile imageFile)
        {
            string filetype = imageFile.FileName.Substring(imageFile.FileName.LastIndexOf('.') + 1);
            Guid guid = Guid.NewGuid();
            string path = $"/images/{guid}.{filetype}";
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return path;
        }
    }
}
